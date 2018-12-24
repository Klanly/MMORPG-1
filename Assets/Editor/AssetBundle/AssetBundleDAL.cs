// **********************************************************************
// Copyright (C) 2018 EastHill Group .Co .Ltd
//
// 文件名(File Name):             AssetBundleDAL.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2018/12/24 16:9:0
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;

/// <summary>
/// 管理类
/// </summary>
public class AssetBundleDAL
{
    /// <summary>
    /// xml路径；读取文件时的路径
    /// </summary>
    private string m_Path;

    /// <summary>
    /// 返回的数据集合
    /// </summary>
    private List<AssetBundleEntity> m_List = null;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="path">外部传过来的path</param>
    public AssetBundleDAL(string path)
    {
        m_Path = path;
        //实例化集合
        m_List = new List<AssetBundleEntity>();
    }

    /// <summary>
    /// 返回xml数据（m_List 集合）
    /// </summary>
    /// <returns></returns>
    public List<AssetBundleEntity> GetList()
    {
        //返回前先清空
        m_List.Clear();

        //======================读取xml 把数据添加到m_List里

        //将xml数据读成XDocument对象
        XDocument xDoc = XDocument.Load(m_Path);
        XElement root = xDoc.Root;

        XElement assetBundleNode = root.Element("AssetBundle");

        IEnumerable<XElement> lst = assetBundleNode.Elements("Item");

        int index = 0;
        //编辑器模式中可以使用foreach
        foreach (XElement item in lst)
        {
            AssetBundleEntity entity = new AssetBundleEntity();
            entity.Key = "key" + ++index;
            entity.Name = item.Attribute("Name").Value;
            entity.Tag = item.Attribute("Tag").Value;
            entity.Version = item.Attribute("Version").Value.ToInt();
            entity.Size = item.Attribute("Size").Value.ToLong();
            entity.ToPath = item.Attribute("ToPath").Value;

            IEnumerable<XElement> pathList = item.Elements("Path");
            foreach (XElement path in pathList)
            {
                entity.PathList.Add(string.Format("Asset/{0}", path.Attribute("Value").Value));
            }

            m_List.Add(entity);
        }

        return m_List;
    }
}
