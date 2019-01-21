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

public class AccountCtrl : Singleton<AccountCtrl>,ISystemCtrl
{
    #region 属性
    /// <summary>
    /// 登录窗口视图
    /// </summary>
    private UILogOnView m_LogOnView;
    /// <summary>
    /// 注册窗口视图
    /// </summary>
    private UIRegView m_RegView;
    #endregion

    #region 构造函数
    public AccountCtrl()
    {
        //监听登录窗口按钮点击
        UIDispatcher.Instance.AddEventListener(ConstDefine.UILogOnView_btnLogOn, LogOnViewBtnLogOnClick);
        UIDispatcher.Instance.AddEventListener(ConstDefine.UILogOnView_btnToReg, LogOnViewBtnToRegClick);

        //监听注册窗口按钮点击
        UIDispatcher.Instance.AddEventListener(ConstDefine.UIRegView_btnReg, RegViewBtnRegClick);
        UIDispatcher.Instance.AddEventListener(ConstDefine.UIRegView_btnToLogOn, RegViewBtnToLogOnClick);
    }
    #endregion

    #region LogOnViewBtnLogOnClick 登录视图 登录按钮点击
    /// <summary>
    /// 登录视图 登录按钮点击
    /// </summary>
    /// <param name="param"></param>
    private void LogOnViewBtnLogOnClick(object[] param)
    {
        Debug.Log("LogOnViewBtnLogOnClick");
    }
    #endregion

    #region LogOnViewBtnToRegClick 登录视图 注册按钮点击
    /// <summary>
    /// 登录视图 注册按钮点击
    /// </summary>
    /// <param name="param"></param>
    private void LogOnViewBtnToRegClick(object[] param)
    {
        m_LogOnView.Close(true);
        //m_LogOnView.NextOpenWindow = WindowUIType.Reg;
        //WindowUIMgr.Instance.OpenWindow(WindowUIType.Reg);

    }
    #endregion

    #region RegViewBtnRegClick 注册视图 注册按钮点击
    /// <summary>
    /// 注册视图 注册按钮点击
    /// </summary>
    /// <param name="param"></param>
    private void RegViewBtnRegClick(object[] param)
    {
        
    }
    #endregion

    #region RegViewBtnToLogOnClick 注册视图 返回登录按钮点击
    /// <summary>
    /// 注册视图 返回登录按钮点击
    /// </summary>
    /// <param name="param"></param>
    private void RegViewBtnToLogOnClick(object[] param)
    {
        m_RegView.Close(true);
    }
    #endregion

    #region OpenLogOnView 打开登录视图
    /// <summary>
    /// 打开登录视图
    /// </summary>
    public void OpenLogOnView()
    {
        //获取脚本的引用
        m_LogOnView = UIViewUtil.Instance.OpenWindow(WindowUIType.LogOn).GetComponent<UILogOnView>();
        //窗口关闭后需要执行的动作放在匿名方法中执行
        m_LogOnView.OnViewClose = () =>
          {
              OpenRegView();
          };
    }
    #endregion

    #region OpenRegView 打开注册视图
    /// <summary>
    /// 打开注册视图
    /// </summary>
    public void OpenRegView()
    {
        m_RegView = UIViewUtil.Instance.OpenWindow(WindowUIType.Reg).GetComponent<UIRegView>();
        //在窗口关闭时回调 将登录窗口打开
        m_RegView.OnViewClose = () =>
        {
            OpenLogOnView();
        };
    }
    #endregion

    public void OpenView(WindowUIType type)
    {
        switch (type)
        {
            case WindowUIType.LogOn:
                OpenLogOnView();
                break;
            case WindowUIType.Reg:
                OpenRegView();
                break;
        }
    }

    #region Dispose 释放
    /// <summary>
    /// 释放
    /// </summary>
    public override void Dispose()
    {
        base.Dispose();
        //移除登录窗口按钮点击监听
        UIDispatcher.Instance.RemoveEventListener(ConstDefine.UILogOnView_btnLogOn, LogOnViewBtnLogOnClick);
        UIDispatcher.Instance.RemoveEventListener(ConstDefine.UILogOnView_btnToReg, LogOnViewBtnToRegClick);

        //移除注册窗口按钮点击监听
        UIDispatcher.Instance.RemoveEventListener(ConstDefine.UIRegView_btnReg, RegViewBtnRegClick);
        UIDispatcher.Instance.RemoveEventListener(ConstDefine.UIRegView_btnToLogOn, RegViewBtnToLogOnClick);
    }
    #endregion
}
