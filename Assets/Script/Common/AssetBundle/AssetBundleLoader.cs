// **********************************************************************
// Copyright (C) 2018 EastHill Group .Co .Ltd
//
// 文件名(File Name):             AssetBundleLoader.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2018-12-26 11:21:45
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************

using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 同步加载资源包
/// </summary>
public class AssetBundleLoader : IDisposable
{
    private AssetBundle bundle;

    public AssetBundleLoader(string assetBundlePath)
    {
        //完整路径
        string fullPath = LocalFileMgr.Instance.LocalFilePath + assetBundlePath;
        //加载包
        bundle = AssetBundle.LoadFromMemory(LocalFileMgr.Instance.GetBuffer(fullPath));

    }

    //读取包中的内容；包中有很多内容，可能其中有多个角色，因此需要把包分别读取(使用泛型，这样做在读取时可以指定类型(where T : Object表示传递的是Object类型))
    public T LoadAsset<T>(string name) where T : UnityEngine.Object
    {
        if (bundle == null) return default(T);
        return bundle.LoadAsset(name) as T;
    }

    //卸载bundle时卸载资源（继承自System.IDisposable）
    public void Dispose()
    {
        if (bundle != null) bundle.Unload(false);
    }
}
