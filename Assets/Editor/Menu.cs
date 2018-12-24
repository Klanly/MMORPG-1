// **********************************************************************
// Copyright (C) 2018 EastHill Group .Co .Ltd
//
// 文件名(File Name):             Menu.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2018/12/21 13:52:7
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************

using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class Menu
{
    /// <summary>
    /// 打开窗口
    /// </summary>
    [MenuItem("YouyouTools/Settings")]
    public static void Settings()
    {
        SettingsWindow win = (SettingsWindow)EditorWindow.GetWindow(typeof(SettingsWindow));
        //标题
        win.titleContent = new GUIContent("全局设置");
        win.Show();
    }

    [MenuItem("YouyouTools/AssetBundleCreate")]
    public static void AssetBundleCreate()
    {
        AssetBundleWindow win = EditorWindow.GetWindow<AssetBundleWindow>();
        win.titleContent = new GUIContent("资源打包");
        win.Show();
    }
}
