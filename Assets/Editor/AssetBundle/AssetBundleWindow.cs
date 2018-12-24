// **********************************************************************
// Copyright (C) 2018 EastHill Group .Co .Ltd
//
// 文件名(File Name):             AssetBundleWindow.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2018/12/24 17:52:18
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************

using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// AssetBundle管理窗口
/// </summary>
public class AssetBundleWindow : EditorWindow
{
    private AssetBundleDAL dal;
    private List<AssetBundleEntity> m_List;
    private Dictionary<string, bool> m_Dic;//与之前的做法类似，目的是为了知道选中时哪些被选中

    private string[] arrTag = {"All","Scene","Role","Effect","Audio","None" };
    private int tagIndex = 0;

    private string[] arrBuildTarget = { "Windows", "Android", "iOS" };

    //打包时希望当前为哪个平台时，哪个平台即为默认平台，可以使用宏定义
#if UNITY_STANDALONE_WIN
    private BuildTarget target = BuildTarget.StandaloneWindows;
    private int buildTargetIndex = 0;
#elif UNITY_ANDROID
    private BuildTarget target = BuildTarget.Android;
    private int buildTargetIndex = 1;
#elif UNITY_IPHONE
    private BuildTarget target = BuildTarget.iOS;
    private int buildTargetIndex = 2;
#endif

    /// <summary>
    /// 构造函数
    /// </summary>
    public AssetBundleWindow()
    {
        string xmlPath = Application.dataPath + @"\Editor\AssetBundle\AssetBundleConfig.xml";
        dal = new AssetBundleDAL(xmlPath);
        m_List = dal.GetList();

        //实例化字典，并添加数据
        m_Dic = new Dictionary<string, bool>();

        for(int i = 0; i < m_List.Count; i++)
        {
            m_Dic[m_List[i].Key] = true;//默认都选中
        }
    }

    /// <summary>
    /// 绘制窗口
    /// </summary>
    void OnGUI()
    {
        if (m_List == null) return;

        GUILayout.BeginHorizontal("box");

        //绘制下拉菜单
        tagIndex = EditorGUILayout.Popup(tagIndex, arrTag, GUILayout.Width(100));

        GUILayout.EndHorizontal();
    }
}
