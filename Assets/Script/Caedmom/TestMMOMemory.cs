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
        NetWorkSocket.Instance.Connect("192.168.20.129", 1011);

        //2.发送消息
        using(MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUTF8String("你好啊");

            NetWorkSocket.Instance.SendMsg(ms.ToArray());
        }
    }
}

