// **********************************************************************
// Copyright (C) 2018 EastHill Group .Co .Ltd
//
// 文件名(File Name):             TestScene048.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2018/12/24 12:6:32
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************

using UnityEngine;
using System.Collections;
using System;

public class TestScene048 : MonoBehaviour
{

    void Start()
    {
        //异步加载
        //AssetBundleLoaderAsync async = AssetBundleMgr.Instance.LoadAsync(@"Role\role_mainplayer.assetbundle", "Role_MainPlayer");
        //async.OnLoadComplete = OnLoadComplete;

        //AssetBundleMgr.Instance.LoadAsync(@"Role\role_mainplayer.assetbundle", "Role_MainPlayer").OnLoadComplete = OnLoadComplete;

        AssetBundleMgr.Instance.LoadAsync(@"Role\role_mainplayer.assetbundle", "Role_MainPlayer").OnLoadComplete = (UnityEngine.Object obj) =>
        {
            Instantiate(obj);
        };
    }

    //private void OnLoadComplete(UnityEngine.Object obj)
    //{
    //    //Instantiate((GameObject)obj);
    //    Instantiate(obj);
    //}
}
