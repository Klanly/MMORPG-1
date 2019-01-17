// **********************************************************************
// Copyright (C) 2018 EastHill Group .Co .Ltd
//
// 文件名(File Name):             UIViewMgr.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2019-01-17 12:13:22
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIViewMgr : Singleton<UIViewMgr>
{
    private Dictionary<WindowUIType, ISystemCtrl> m_SystemCtrlDic = new Dictionary<WindowUIType, ISystemCtrl>();

    public UIViewMgr()
    {
        m_SystemCtrlDic.Add(WindowUIType.LogOn, AccountCtrl.Instance);
        m_SystemCtrlDic.Add(WindowUIType.Reg, AccountCtrl.Instance);
    }

    /// <summary>
    /// 打开视图
    /// </summary>
    /// <param name="type"></param>
    public void OpenWindow(WindowUIType type)
    {
        m_SystemCtrlDic[type].OpenView(type);
    }
}
