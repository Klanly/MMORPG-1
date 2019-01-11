// **********************************************************************
// Copyright (C) 2018 EastHill Group .Co .Ltd
//
// 文件名(File Name):             UIWindowViewBase.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2019-01-08 19:29:06
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************

using UnityEngine;
using System.Collections;

public class UIWindowViewBase : UIViewBase
{
    /// <summary>
    /// 挂点类型
    /// </summary>
    [SerializeField]
    public WindowUIContainerType containerType = WindowUIContainerType.Center;

    /// <summary>
    /// 打开方式
    /// </summary>
    [SerializeField]
    public WindowShowStyle showStyle = WindowShowStyle.Normal;

    /// <summary>
    /// 打开或关闭动画效果持续时间
    /// </summary>
    [SerializeField]
    public float duration = 0.2f;

    /// <summary>
    /// 当前窗口类型
    /// </summary>
    [HideInInspector]
    public WindowUIType CurrentUIType;

    /// <summary>
    /// 下一个要打开的窗口
    /// </summary>
    //protected WindowUIType NextOpenWindow = WindowUIType.None;
    public WindowUIType NextOpenWindow = WindowUIType.None;

    protected override void OnBtnClick(GameObject go)
    {
        base.OnBtnClick(go);
        if (go.name.Equals("btnClose", System.StringComparison.CurrentCultureIgnoreCase))
        {
            Close();
        }
    }

    /// <summary>
    /// 关闭窗口
    /// </summary>
    //protected virtual void Close()
    public virtual void Close()
    {
        WindowUIMgr.Instance.CloseWindow(CurrentUIType);
    }

    /// <summary>
    /// 销毁之前执行
    /// </summary>
    protected override void BeforeOnDestroy()
    {
        LayerUIMgr.Instance.CheckOpenWindow();
        if (NextOpenWindow == WindowUIType.None) return;
        WindowUIMgr.Instance.OpenWindow(NextOpenWindow);
    }
}
