// **********************************************************************
// Copyright (C) 2018 EastHill Group .Co .Ltd
//
// 文件名(File Name):             TestSingletonMono.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2018/12/18 19:52:58
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************

using UnityEngine;
using System.Collections;

public class TestSingletonMono : MonoBehaviour
{
    void Start()
    {
        NetWorkSocket net1 = NetWorkSocket.Instance;
        NetWorkHttp net2 = NetWorkHttp.Instance;        
    }
}
