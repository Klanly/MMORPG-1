// **********************************************************************
// Copyright (C) 2018 EastHill Group .Co .Ltd
//
// 文件名(File Name):             AssetBundleEntity.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2018/12/24 16:8:38
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// AssetBundle实体
/// </summary>
public class AssetBundleEntity
{
    /// <summary>
    /// 用于打包时选定，唯一的Key
    /// </summary>
    public string Key;

    /// <summary>
    /// 名称
    /// </summary>
    public string Name;

    /// <summary>
    /// 标记
    /// </summary>
    public string Tag;

    /// <summary>
    /// 版本号
    /// </summary>
    public int Version;

    /// <summary>
    /// 大小（K）
    /// </summary>
    public long Size;

    /// <summary>
    /// 打包保存的路径
    /// </summary>
    public string ToPath;

    private List<string> m_PathList = new List<string>();
    /// <summary>
    /// 路径集合
    /// </summary>
    public List<string> PathList
    {
        get { return m_PathList; }
    }
}
