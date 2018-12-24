// **********************************************************************
// Copyright (C) 2018 EastHill Group .Co .Ltd
//
// 文件名(File Name):             SettingsWindow.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2018/12/21 14:2:4
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************

using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class SettingsWindow : EditorWindow
{
    //定义项的列表
    private List<MacroItem> m_List = new List<MacroItem>();
    //创建字典
    private Dictionary<string, bool> m_Dic = new Dictionary<string, bool>();

    private string m_Macro = null;

    /// <summary>
    /// 构造函数，在其中对项进行初始化
    /// </summary>
    public SettingsWindow()
    {
        //初始化时取自定义宏
        m_Macro = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);

        m_List.Clear();
        //往列表中添加项（调试相关，统计相关）
        m_List.Add(new MacroItem() { Name = "DEBUG_MODEL", DisplayName = "调试模式", IsDebug = true, IsRelease = false });
        m_List.Add(new MacroItem() { Name = "DEBUG_LOG", DisplayName = "打印日志", IsDebug = true, IsRelease = false });
        m_List.Add(new MacroItem() { Name = "STAT_ID", DisplayName = "开启统计", IsDebug = false, IsRelease = true });

        //将Name添加到字典中
        for (int i = 0; i < m_List.Count; i++)
        {
            //取到值且包含指定项则为true
            if (!string.IsNullOrEmpty(m_Macro) && m_Macro.IndexOf(m_List[i].Name) != -1)
            {
                m_Dic[m_List[i].Name] = true;
            }
            else
            {
                m_Dic[m_List[i].Name] = false;//默认值为false
            }

        }
    }

    void OnGUI()
    {
        for (int i = 0; i < m_List.Count; i++)
        {
            //Bengin End成对出现
            EditorGUILayout.BeginHorizontal("box");
            m_Dic[m_List[i].Name] = GUILayout.Toggle(m_Dic[m_List[i].Name], m_List[i].DisplayName);
            EditorGUILayout.EndHorizontal();
        }

        //按钮排成一行
        EditorGUILayout.BeginHorizontal();

        //点击按钮保存项
        if (GUILayout.Button("保存", GUILayout.Width(100)))
        {
            SaveMacro();
        }


        if (GUILayout.Button("调试模式", GUILayout.Width(100)))
        {
            for(int i = 0; i < m_List.Count; i++)
            {
                m_Dic[m_List[i].Name] = m_List[i].IsDebug;
            }
            SaveMacro();
        }
        if (GUILayout.Button("发布模式", GUILayout.Width(100)))
        {
            for (int i = 0; i < m_List.Count; i++)
            {
                m_Dic[m_List[i].Name] = m_List[i].IsRelease;
            }
            SaveMacro();
        }

        EditorGUILayout.EndHorizontal();


    }

    private void SaveMacro()
    {
        m_Macro = string.Empty;
        foreach(var item in m_Dic)
        {
            if (item.Value)
            {
                m_Macro += string.Format("{0};",item.Key);
            }
        }
        //保存值
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, m_Macro);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, m_Macro);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, m_Macro);
    }

    //添加设置项，定义类
    /// <summary>
    /// 宏项目
    /// </summary>
    public class MacroItem
    {
        /// <summary>
        /// 名称
        /// </summary>        
        public string Name;

        /// <summary>
        /// 显示的名称
        /// </summary>
        public string DisplayName;

        /// <summary>
        /// 是否调试项
        /// </summary>
        public bool IsDebug;

        /// <summary>
        /// 是否发布项
        /// </summary>
        public bool IsRelease;
    }
}
