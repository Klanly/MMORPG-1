#region 模块信息
// **********************************************************************
// Copyright (C) 2017 The company name
//
// 文件名(File Name):             SpriteTitleChange.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2018/11/27 12:15:27
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;

public class SpriteTitleChange : UnityEditor.AssetModificationProcessor
{
    private static void OnWillCreateAsset(string path)
    {
        path = path.Replace(".meta", "");
        if (path.EndsWith(".cs"))
        {
            string allText = File.ReadAllText(path);
            allText = allText.Replace("#AuthorName#", "Caedmom")
                              .Replace("#CreateTime#", System.DateTime.Now.Year + "/" + System.DateTime.Now.Month
                + "/" + System.DateTime.Now.Day + " " + System.DateTime.Now.Hour + ":"
                + System.DateTime.Now.Minute + ":" + System.DateTime.Now.Second);

            File.WriteAllText(path, allText);
        }

    }
}