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
        //监听登录窗口按钮点击
        EventDispatcher.Instance.AddBtnEventListener(ConstDefine.UILogOnView_btnLogOn, LogOnViewBtnLogOnClick);
        EventDispatcher.Instance.AddBtnEventListener(ConstDefine.UILogOnView_btnToReg, LogOnViewBtnToRegClick);

        //监听注册窗口按钮点击
        EventDispatcher.Instance.AddBtnEventListener(ConstDefine.UIRegView_btnReg, RegViewBtnRegClick);
        EventDispatcher.Instance.AddBtnEventListener(ConstDefine.UIRegView_btnToLogOn, RegViewBtnToLogOnClick);
    }

    /// <summary>
    /// 登录视图 登录按钮点击
    /// </summary>
    /// <param name="param"></param>
    private void LogOnViewBtnLogOnClick(object[] param)
    {
        Debug.Log("LogOnViewBtnLogOnClick");
    }

    /// <summary>
    /// 登录视图 注册按钮点击
    /// </summary>
    /// <param name="param"></param>
    private void LogOnViewBtnToRegClick(object[] param)
    {
        //m_LogOnView.Close();
        //m_LogOnView.NextOpenWindow = WindowUIType.Reg;
        WindowUIMgr.Instance.OpenWindow(WindowUIType.Reg);

    }

    /// <summary>
    /// 注册视图 注册按钮点击
    /// </summary>
    /// <param name="param"></param>
    private void RegViewBtnRegClick(object[] param)
    {
        
    }

    /// <summary>
    /// 注册视图 返回登录按钮点击
    /// </summary>
    /// <param name="param"></param>
    private void RegViewBtnToLogOnClick(object[] param)
    {
        WindowUIMgr.Instance.OpenWindow(WindowUIType.LogOn);
        Debug.Log("注册视图 返回登录按钮点击");
    }

    public void OpenLogOnView()
    {
        //获取脚本的引用
        m_LogOnView = WindowUIMgr.Instance.OpenWindow(WindowUIType.LogOn).GetComponent<UILogOnView>();
    }

    public override void Dispose()
    {
        base.Dispose();
        //移除登录窗口按钮点击监听
        EventDispatcher.Instance.RemoveBtnEventListener(ConstDefine.UILogOnView_btnLogOn, LogOnViewBtnLogOnClick);
        EventDispatcher.Instance.RemoveBtnEventListener(ConstDefine.UILogOnView_btnToReg, LogOnViewBtnToRegClick);

        //移除注册窗口按钮点击监听
        EventDispatcher.Instance.RemoveBtnEventListener(ConstDefine.UIRegView_btnReg, RegViewBtnRegClick);
        EventDispatcher.Instance.RemoveBtnEventListener(ConstDefine.UIRegView_btnToLogOn, RegViewBtnToLogOnClick);
    }
}
