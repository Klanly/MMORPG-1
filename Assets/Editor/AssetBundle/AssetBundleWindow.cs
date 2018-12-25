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
using System;
using System.IO;

/// <summary>
/// AssetBundle管理窗口
/// </summary>
public class AssetBundleWindow : EditorWindow
{
    private AssetBundleDAL dal;
    private List<AssetBundleEntity> m_List;
    private Dictionary<string, bool> m_Dic;//与之前的做法类似，目的是为了知道选中时哪些被选中

    private string[] arrTag = { "All", "Scene", "Role", "Effect", "Audio", "None" };
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

    private Vector2 pos;

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

        for (int i = 0; i < m_List.Count; i++)
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

        #region 按钮行
        GUILayout.BeginHorizontal("box");

        //绘制下拉菜单，选择Tag
        tagIndex = EditorGUILayout.Popup(tagIndex, arrTag, GUILayout.Width(100));
        if (GUILayout.Button("选定Tag", GUILayout.Width(100)))
        {
            EditorApplication.delayCall = OnSelectTagCallBack;
        }

        //绘制下拉菜单，选择打包平台
        buildTargetIndex = EditorGUILayout.Popup(buildTargetIndex, arrBuildTarget, GUILayout.Width(100));
        if (GUILayout.Button("选定Target", GUILayout.Width(100)))
        {
            EditorApplication.delayCall = OnSelectTargetCallBack;
        }

        if (GUILayout.Button("打AssetBundle包", GUILayout.Width(200)))
        {
            EditorApplication.delayCall = OnAssetBundleCallBack;
        }

        if (GUILayout.Button("清空AssetBundle包", GUILayout.Width(200)))
        {
            EditorApplication.delayCall = OnClearAssetBundleCallBack;
        }

        //加空格，使横行绘制完全
        EditorGUILayout.Space();

        GUILayout.EndHorizontal();
        #endregion

        #region 标题
        GUILayout.BeginHorizontal("box");
        GUILayout.Label("包名");
        GUILayout.Label("标记", GUILayout.Width(100));
        GUILayout.Label("保存路径", GUILayout.Width(200));
        GUILayout.Label("版本", GUILayout.Width(100));
        GUILayout.Label("大小", GUILayout.Width(100));
        GUILayout.EndHorizontal();
        #endregion

        #region 内容
        GUILayout.BeginVertical();
        //开启滚动视图
        pos = EditorGUILayout.BeginScrollView(pos);

        //循环项
        for (int i = 0; i < m_List.Count; i++)
        {
            AssetBundleEntity entity = m_List[i];

            GUILayout.BeginHorizontal("box");

            //复选框
            m_Dic[entity.Key] = GUILayout.Toggle(m_Dic[entity.Key], "", GUILayout.Width(20));

            GUILayout.Label(entity.Name);
            GUILayout.Label(entity.Tag, GUILayout.Width(100));
            GUILayout.Label(entity.ToPath, GUILayout.Width(200));
            GUILayout.Label(entity.Version.ToString(), GUILayout.Width(100));
            GUILayout.Label(entity.Size.ToString(), GUILayout.Width(100));

            GUILayout.EndHorizontal();

            foreach (string path in entity.PathList)
            {
                GUILayout.BeginHorizontal("box");
                //40宽度空格
                GUILayout.Space(40);
                //绘制路径
                GUILayout.Label(path);
                GUILayout.EndHorizontal();
            }
        }

        EditorGUILayout.EndScrollView();

        GUILayout.EndVertical();

        #endregion
    }

    /// <summary>
    /// 选定Tag回调
    /// </summary>
    private void OnSelectTagCallBack()
    {
        switch (tagIndex)
        {
            case 0://全选
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = true;
                }
                break;

            case 1://Scene
                foreach (AssetBundleEntity entity in m_List)
                {
                    //忽略大小写，看tag是否为scene
                    m_Dic[entity.Key] = entity.Tag.Equals("Scene", StringComparison.CurrentCultureIgnoreCase);
                }
                break;

            case 2://Role
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("Role", StringComparison.CurrentCultureIgnoreCase);
                }
                break;

            case 3://Effect
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("Effect", StringComparison.CurrentCultureIgnoreCase);
                }
                break;

            case 4://Audio
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("Audio", StringComparison.CurrentCultureIgnoreCase);
                }
                break;

            case 5://None
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = false;
                }
                break;
        }
        Debug.LogFormat("当前选择的Tag:{0}", arrTag[tagIndex]);
    }

    /// <summary>
    /// 选定Target回调
    /// </summary>
    private void OnSelectTargetCallBack()
    {
        switch (buildTargetIndex)
        {
            case 0:
                target = BuildTarget.StandaloneWindows;
                break;

            case 1:
                target = BuildTarget.Android;
                break;

            case 2:
                target = BuildTarget.iOS;
                break;
        }
        Debug.LogFormat("当前选择的BuildTarget:{0}", arrBuildTarget[buildTargetIndex]);
    }

    /// <summary>
    /// 打ab包回调
    /// </summary>
    private void OnAssetBundleCallBack()
    {
        //需要打包的对象
        List<AssetBundleEntity> lstNeedBuild = new List<AssetBundleEntity>();

        foreach(AssetBundleEntity entity in m_List)
        {
            if (m_Dic[entity.Key])
            {
                lstNeedBuild.Add(entity);
            }
        }

        //循环打包
        for(int i = 0; i < lstNeedBuild.Count; i++)
        {
            Debug.LogFormat("正在打包{0}/{1}", i + 1, lstNeedBuild.Count);
            BuildAssetBundle(lstNeedBuild[i]);
        }

        Debug.Log("打包完毕");
    }

    /// <summary>
    /// 打包方法;通过配置文件打包；xml中一个大的item就是一个包
    /// </summary>
    private void BuildAssetBundle(AssetBundleEntity entity)
    {
        AssetBundleBuild[] arrBuild = new AssetBundleBuild[1];

        AssetBundleBuild build = new AssetBundleBuild();

        //包名
        build.assetBundleName = string.Format("{0}.{1}", entity.Name, (entity.Tag.Equals("Scene", StringComparison.CurrentCultureIgnoreCase) ? "unity3d" : "assetbundle"));

        //资源路径
        build.assetNames = entity.PathList.ToArray();

        arrBuild[0] = build;

        //目标路径
        string toPath = Application.dataPath + "/../AssetBundles/" + arrBuildTarget[buildTargetIndex] + entity.ToPath;

        //如果目标路径不存在则创建该路径
        if (!Directory.Exists(toPath))
        {
            Directory.CreateDirectory(toPath);
        }

        BuildPipeline.BuildAssetBundles(toPath, arrBuild, BuildAssetBundleOptions.None, target);
    }

    /// <summary>
    /// 清空ab包回调；把打的包删除
    /// </summary>
    private void OnClearAssetBundleCallBack()
    {
        string path = Application.dataPath + "/../AssetBundles/" + arrBuildTarget[buildTargetIndex];

        if (Directory.Exists(path))
        {
            //加true参数删除子目录
            Directory.Delete(path,true);
        }

        Debug.Log("清除完毕");
    }
}
