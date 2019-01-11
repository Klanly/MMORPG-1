// **********************************************************************
// Copyright (C) 2018 EastHill Group .Co .Ltd
//
// 文件名(File Name):             AccountCtrl.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2019-01-09 12:22:43
// 修改者列表(modifier):
// 模块描述(Module description): 账户控制器
// **********************************************************************

using UnityEngine;
using System.Collections;
using System;

public class AccountCtrl : Singleton<AccountCtrl>
{
    /// <summary>
    /// 登录窗口视图
    /// </summary>
    private UILogOnView m_LogOnView;

    public AccountCtrl()
    {
        EventDispatcher.Instance.AddBtnEventListener(ConstDefine.UILogOnView_btnLogOn, LogOnViewBtnLogOnClick);
        EventDispatcher.Instance.AddBtnEventListener(ConstDefine.UILogOnView_btnToReg, LogOnViewBtnToRegClick);
    }

    private void LogOnViewBtnLogOnClick(object[] param)
    {
        Debug.Log("LogOnViewBtnLogOnClick");
    }

    private void LogOnViewBtnToRegClick(object[] param)
    {
        m_LogOnView.Close();
        m_LogOnView.NextOpenWindow = WindowUIType.Reg;

    }

    public void OpenLogOnView()
    {
        //获取脚本的引用
        m_LogOnView = WindowUIMgr.Instance.OpenWindow(WindowUIType.LogOn).GetComponent<UILogOnView>();
        m_LogOnView = WindowUIMgr.Instance.OpenWindow(WindowUIType.Reg).GetComponent<UILogOnView>();
    }

    public override void Dispose()
    {
        base.Dispose();
        EventDispatcher.Instance.RemoveBtnEventListener(ConstDefine.UILogOnView_btnLogOn, LogOnViewBtnLogOnClick);
        EventDispatcher.Instance.RemoveBtnEventListener(ConstDefine.UILogOnView_btnToReg, LogOnViewBtnToRegClick);
    }
}
