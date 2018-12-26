// **********************************************************************
// Copyright (C) 2018 EastHill Group .Co .Ltd
//
// 文件名(File Name):             LocalFileMgr.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2018-12-26 09:47:27
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************

using UnityEngine;
using System.Collections;
using System.IO;

/// <summary>
/// 本地文件管理
/// </summary>
public class LocalFileMgr : Singleton<LocalFileMgr>
{
    //定义路径
#if UNITY_EDITOR //编辑器模式

#if UNITY_STANDALONE_WIN
    public readonly string LocalFilePath = Application.dataPath + "/../AssetBundles/Windows/";
#elif UNITY_ANDROID
    public readonly string LocalFilePath = Application.dataPath + "/../AssetBundles/Android/";
#elif UNITY_IPHONE
    public readonly string LocalFilePath = Application.dataPath + "/../AssetBundles/iOS/";
#endif

#elif UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_WIN //发布模式
    public readonly string LocalFilePath = Application.persistentDataPath + "/";
#endif
    /// <summary>
    /// GameDataTableParser读取本地文件到byte[]数组
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public byte[] GetBuffer(string path)
    {
        byte[] buffer = null;
        using (FileStream fs = new FileStream(path, FileMode.Open))
        {
            buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
        }
        return buffer;
    }
}
