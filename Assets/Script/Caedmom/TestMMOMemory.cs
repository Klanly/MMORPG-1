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
    }

    /// <summary>
    /// 2.发送消息
    /// </summary>
    /// <param name="msg"></param>
    private void Send(string msg)
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUTF8String(msg);

            NetWorkSocket.Instance.SendMsg(ms.ToArray());
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Send("Hello,A!");
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            Send("Hello,B!");
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            for(int i = 0; i < 10; i++)
            {
                Send("Hello,C!"+i);
            }     
        }
    }
}

