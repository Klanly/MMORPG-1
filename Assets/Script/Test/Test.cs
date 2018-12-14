// **********************************************************************
// Copyright (C) 2018 EastHill Group .Co .Ltd
//
// 文件名(File Name):             Test.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2018/12/14 11:6:19
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************

using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour
{
    void Start()
    {
        //1.连接到服务器
        NetWorkSocket.Instance.Connect(GlobalInit.SocketIP, GlobalInit.Port);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Mail_Get_DetailProto proto = new Mail_Get_DetailProto();
            NetWorkSocket.Instance.SendMsg(proto.ToArray());
        }
    }
}