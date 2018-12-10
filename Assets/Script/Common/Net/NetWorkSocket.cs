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

public class NetWorkSocket : MonoBehaviour
{

    #region 单例
    private static NetWorkSocket instance;

    public static NetWorkSocket Instance
    {
        get
        {
            if (instance == null)
            {
                //instance = new NetWorkHttp();
                //继承自MonoBehaviour的单例写法
                GameObject obj = new GameObject("NetWorkSocket");
                //不释放/不销毁这个物体
                DontDestroyOnLoad(obj);
                instance = obj.GetOrCreatComponent<NetWorkSocket>();
            }
            return instance;
        }
    }
    #endregion

    //定义缓冲区
    //private byte[] buffer = new byte[10240];

    //定义发送消息队列并实例化
    private Queue<byte[]> m_SendQueue = new Queue<byte[]>();

    //定义没有参数的委托，用于检查队列
    private Action m_CheckSendQueue;

    /// <summary>
    /// 客户端Socket
    /// </summary>
    private Socket m_Client;

    void OnDestroy()
    {
        if(m_Client !=null && m_Client.Connected)
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
    public void Connect(string ip,int port)
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

            Debug.Log("连接成功");
        }
        catch (Exception ex)
        {
            Debug.Log("连接失败"+ex.Message);
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
        using(MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            //往ms中写入数据
            ms.WriteUShort((ushort)data.Length);
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
}
