// **********************************************************************
// Copyright (C) 2018 The company name
//
// 文件名(File Name):             NetWorkSocket.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2018/12/10 15:30:15
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************

using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System;
using System.Net;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// 网络传输Socket
/// </summary>
public class NetWorkSocket : SingletonMono<NetWorkSocket>
{

    //定义缓冲区
    //private byte[] buffer = new byte[10240];

    #region 发送消息所需变量
    //定义发送消息队列并实例化
    private Queue<byte[]> m_SendQueue = new Queue<byte[]>();

    //定义没有参数的委托，用于检查队列
    private Action m_CheckSendQueue;

    //压缩数组的长度界限（大于200则压缩）
    private const int m_CompressLength = 200;
    #endregion

    #region 接收消息所需变量
    //接收数据包的字节数组缓冲区
    private byte[] m_ReceiveBuffer = new byte[2048];

    //定义MMO_MemoryStream属性,接收数据包的缓冲数据流
    private MMO_MemoryStream m_ReceiveMS = new MMO_MemoryStream();

    //接收消息的队列
    private Queue<byte[]> m_ReceiveQueue = new Queue<byte[]>();

    //数据非常多时 不能一次性读完
    private int m_ReceiveCount = 0;
    #endregion

    /// <summary>
    /// 客户端Socket
    /// </summary>
    private Socket m_Client;

    protected override void OnStart()
    {
        base.OnStart();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        #region 从队列中获取数据
        while (true)
        {
            //每帧读取5个数据
            if (m_ReceiveCount <= 5)
            {
                m_ReceiveCount++;
                lock (m_ReceiveQueue)
                {
                    //如果队列中有数据，则接收数据
                    if (m_ReceiveQueue.Count > 0)
                    {
                        //得到队列中的数据包；将数据存入数组
                        byte[] buffer = m_ReceiveQueue.Dequeue();

                        //异或之后的数组；数据包内容
                        byte[] bufferNew = new byte[buffer.Length - 3];

                        bool isCompress = false;
                        ushort crc = 0;

                        //读取协议编号
                        //ushort protoCode = 0;
                        //数据包内容
                        //byte[] protoContent = new byte[buffer.Length - 2];

                        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
                        {
                            isCompress = ms.ReadBool();
                            //传递过来的CRC
                            crc = ms.ReadUShort();
                            ms.Read(bufferNew, 0, bufferNew.Length);
                        }

                        //计算CRC
                        int newCrc = Crc16.CalculateCrc16(bufferNew);

                        Debug.Log("CRC=" + crc);
                        Debug.Log("NewCRC=" + newCrc);

                        if (newCrc == crc)
                        {
                            //异或，得到原始数据
                            bufferNew = SecurityUtil.Xor(bufferNew);

                            //解压缩
                            if (isCompress)
                            {
                                bufferNew = ZlibHelper.DeCompressBytes(bufferNew);
                            }

                            ushort protoCode = 0;
                            byte[] protoContent = new byte[buffer.Length - 2];
                            //解析
                            using (MMO_MemoryStream ms = new MMO_MemoryStream(bufferNew))
                            {
                                protoCode = ms.ReadUShort();
                                ms.Read(protoContent, 0, protoContent.Length);

                                SocketDispatcher.Instance.Dispatch(protoCode, protoContent);
                            }
                        }
                        else
                        {
                            break;
                        }

                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                m_ReceiveCount = 0;
                break;
            }
        }
        #endregion
    }

    protected override void BeforeOnDestroy()
    {
        base.BeforeOnDestroy();

        if (m_Client != null && m_Client.Connected)
        {
            m_Client.Shutdown(SocketShutdown.Both);
            m_Client.Close();
        }
    }


    #region Connect 连接到Socket服务器
    /// <summary>
    /// 连接到Socket服务器
    /// </summary>
    /// <param name="ip">服务器ip</param>
    /// <param name="port">端口号</param>
    public void Connect(string ip, int port)
    {
        //如果Socket不存在或已在连接状态，直接返回return
        if (m_Client != null && m_Client.Connected) return;

        m_Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //尝试连接
        try
        {
            //进行连接
            m_Client.Connect(new IPEndPoint(IPAddress.Parse(ip), port));

            //在进行客户端连接时，监听用于检查队列的委托
            m_CheckSendQueue = OnCheckSendQueueCallBack;

            //连接成功时接收消息
            ReceiveMsg();
            Debug.Log("连接成功");
        }
        catch (Exception ex)
        {
            Debug.Log("连接失败" + ex.Message);
        }
    }
    #endregion

    #region OnCheckSendQueueCallBack 检查队列的委托回调
    /// <summary>
    /// 检查队列的委托回调
    /// </summary>
    private void OnCheckSendQueueCallBack()
    {
        //在这个类监听了委托，所以会进入委托的回调，回调时也加锁(锁住队列)
        lock (m_SendQueue)
        {
            //如果队列中有数据包 则发送数据包
            if (m_SendQueue.Count > 0)
            {
                //从队列中取出数据并发送数据包
                Send(m_SendQueue.Dequeue());
            }

        }
    }
    #endregion

    #region MakeData 封装数据包
    /// <summary>
    /// 封装数据包
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private byte[] MakeData(byte[] data)
    {
        byte[] retBuffer = null;
        //0.压缩标志 //数据包长度大于设定长度则压缩
        bool isCompress = data.Length > m_CompressLength ? true : false;
        if (isCompress)
        {
            data = ZlibHelper.CompressBytes(data);
        }

        //1.异或
        data = SecurityUtil.Xor(data);

        //2.CRC校验
        ushort crc = Crc16.CalculateCrc16(data);

        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            //3.写入包长度
            //往ms中写入数据(包长度),数据的长度+压缩标志（1byte）+CRC校验（UShort（2byte））
            ms.WriteUShort((ushort)(data.Length + 3));
            //往ms中写入数据(压缩标识)
            ms.WriteBool(isCompress);
            ms.WriteUShort(crc);
            ms.Write(data, 0, data.Length);

            retBuffer = ms.ToArray();
        }

        return retBuffer;
    }
    #endregion

    #region SendMsg “发送消息” (把消息加入队列)
    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="buffer"></param>
    public void SendMsg(byte[] buffer)
    {
        //传入的string即是包体，需要转为byte[]数组;得到包体的byte数组
        //byte[] data = Encoding.UTF8.GetBytes(msg);

        //得到封装后的数据包
        byte[] sendBuffer = MakeData(buffer);

        //使用锁，如果多人同时访问，则会有并发问题
        lock (m_SendQueue)
        {
            //把数据包加入队列
            m_SendQueue.Enqueue(sendBuffer);

            //启动委托(执行委托)
            m_CheckSendQueue.BeginInvoke(null, null);
        }
    }
    #endregion

    #region Send 真正发送数据包到服务器
    /// <summary>
    /// 真正发送数据包到服务器
    /// </summary>
    /// <param name="buffer"></param>
    private void Send(byte[] buffer)
    {
        //SendCallBack 回调
        m_Client.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, SendCallBack, m_Client);
    }
    #endregion

    #region SendCallBack 发送数据包的回调
    /// <summary>
    /// 发送数据包的回调
    /// </summary>
    /// <param name="ar"></param>
    private void SendCallBack(IAsyncResult ar)
    {
        m_Client.EndSend(ar);

        //继续检查队列(执行回调方法即可)
        OnCheckSendQueueCallBack();
    }
    #endregion

    //=================接收消息====================
    #region ReceiveMsg 接收数据
    /// <summary>
    ///接收数据
    /// </summary>
    private void ReceiveMsg()
    {
        //异步接收数据
        m_Client.BeginReceive(m_ReceiveBuffer, 0, m_ReceiveBuffer.Length, SocketFlags.None, ReceiveCallBack, m_Client);
    }
    #endregion

    #region ReceiveCallBack 接收数据回调函数
    /// <summary>
    /// 接收数据回调函数
    /// </summary>
    /// <param name="ar"></param>
    private void ReceiveCallBack(IAsyncResult ar)
    {
        try
        {
            int len = m_Client.EndReceive(ar);

            if (len > 0)
            {
                //已经接收到数据

                //把接收到的数据 写入缓冲数据流的尾部
                m_ReceiveMS.Position = m_ReceiveMS.Length;

                //把指定长度的字节 写入数据流
                m_ReceiveMS.Write(m_ReceiveBuffer, 0, len);

                //假设只有一条消息，这个就是我们收到的消息
                //byte[] buffer = m_ReceiveMS.ToArray();
                //Console.WriteLine("buffer=" + buffer.Length);

                //如果缓存数据流的长度>2 说明至少有个不完整的包过来了
                //这里为什么是2 因为客户端封装数据包用的是UShort 长度是2
                if (m_ReceiveMS.Length > 2)
                {
                    //进行循环 拆分数据包                 
                    while (true)
                    {
                        //把数据流指针位置放在0处
                        m_ReceiveMS.Position = 0;

                        //currMsgLen = 包体的长度
                        int currMsgLen = m_ReceiveMS.ReadUShort();

                        //currFullMsgLen 总包的长度 = 包头长度+包体长度
                        int currFullMsgLen = 2 + currMsgLen;

                        //如果数据流的长度>=整包的长度 说明至少收到了一个完整包
                        //如果有一个完整包 则可以进行拆包
                        if (m_ReceiveMS.Length >= currFullMsgLen)
                        {
                            //至少收到一个完整包

                            //定义包体的byte[]数组
                            byte[] buffer = new byte[currMsgLen];

                            //把数据流指针放到2的位置 也就是包体的位置
                            m_ReceiveMS.Position = 2;

                            //把包体读到byte[]数组
                            m_ReceiveMS.Read(buffer, 0, currMsgLen);

                            //将数据包压入队列
                            lock (m_ReceiveQueue)
                            {
                                m_ReceiveQueue.Enqueue(buffer);
                            }
                            //buffer这个byte[]数组就是包体 也就是我们要的数据
                            //using (MMO_MemoryStream ms2 = new MMO_MemoryStream(buffer))
                            //{
                            //    string msg = ms2.ReadUTF8String();
                            //    Console.WriteLine(msg);
                            //}

                            //=============处理剩余字节数组数据=================

                            //剩余字节长度
                            int remainLen = (int)m_ReceiveMS.Length - currFullMsgLen;
                            if (remainLen > 0)
                            {
                                //指针放到第一个包的尾部
                                m_ReceiveMS.Position = currFullMsgLen;

                                //定义剩余字节数组
                                byte[] remainBuffer = new byte[remainLen];

                                //把数据流读到剩余字节数组
                                m_ReceiveMS.Read(remainBuffer, 0, remainLen);

                                //清空数据流
                                m_ReceiveMS.Position = 0;
                                m_ReceiveMS.SetLength(0);

                                //把剩余字节数组重新写入数据流
                                m_ReceiveMS.Write(remainBuffer, 0, remainBuffer.Length);

                                remainBuffer = null;
                            }
                            else
                            {
                                //没有剩余字节 则直接清空数据流
                                m_ReceiveMS.Position = 0;
                                m_ReceiveMS.SetLength(0);

                                break;
                            }

                        }
                        else
                        {
                            //还没有收到完整包
                            break;
                        }
                    }
                }

                //进行下一步接收数据包
                ReceiveMsg();


            }
            else
            {
                //客户端断开连接
                Debug.Log(string.Format("服务器{0}断开连接", m_Client.RemoteEndPoint.ToString()));

            }
        }
        catch (Exception ex)
        {
            //客户端断开连接
            Debug.Log(string.Format("客户端{0}断开连接", m_Client.RemoteEndPoint.ToString()));

        }

    }
    #endregion
}
