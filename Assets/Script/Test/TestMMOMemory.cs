// **********************************************************************
// Copyright (C) 2018 The company name
//
// 文件名(File Name):             TestMMOMemory.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2018/11/29 11:22:43
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using LitJson;

public class TestMMOMemory : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        //1.连接到服务器
        NetWorkSocket.Instance.Connect(GlobalInit.SocketIP, GlobalInit.Port);

        //监听委托
        //GlobalInit.Instance.OnReceiveProto = OnReceiveProtoCallBack;
    }

    //委托回调
    private void OnReceiveProtoCallBack(ushort protoCode, byte[] buffer)
    {
        Debug.Log("protoCode=" + protoCode);
        if (protoCode == ProtoCodeDef.Mail_Get_Detail)
        {
            Mail_Get_DetailProto mailProto = Mail_Get_DetailProto.GetProto(buffer);
            Debug.Log("IsSuccess=" + mailProto.IsSuccess);
            if (mailProto.IsSuccess)
            {
                Debug.Log("Name=" + mailProto.Name);
            }
            else
            {
                Debug.Log("ErrorCode=" + mailProto.ErrorCode);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            TestProto proto = new TestProto();
            proto.Id = 100;
            proto.Name = "测试协议";
            proto.Price = 66.6f;
            proto.Type = 80;

            //发送
            NetWorkSocket.Instance.SendMsg(proto.ToArray());
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Mail_Get_DetailProto proto = new Mail_Get_DetailProto();
            NetWorkSocket.Instance.SendMsg(proto.ToArray());
        }
    }
}

