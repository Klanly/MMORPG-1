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
        m_Macro = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone);

        m_List.Clear();
        //往列表中添加项（调试相关，统计相关）
        m_List.Add(new MacroItem() { Name = "DEBUG_MODEL", DisplayName = "调试模式", IsDebug = true, IsRelease = false });
        m_List.Add(new MacroItem() { Name = "DEBUG_Log", DisplayName = "打印日志", IsDebug = true, IsRelease = false });
        m_List.Add(new MacroItem() { Name = "STAT_ID", DisplayName = "开启统计", IsDebug = false, IsRelease = true });

        //将Name添加到字典中
        for(int i = 0; i < m_List.Count; i++)
        {
            m_Dic[m_List[i].Name] = false;//默认值为false
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
