// **********************************************************************
// Copyright (C) 2018 EastHill Group .Co .Ltd
//
// 文件名(File Name):             DispatcherBase.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2019-01-18 10:50:31
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DispatcherBase<T> : IDisposable where T : new()
{
    #region 单例
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }

    public virtual void Dispose()
    {

    }
    #endregion

    //按钮点击事件委托原型
    public delegate void OnActionHandler(params System.Object[] param);
    //定义字典并实例化
    private Dictionary<string, List<OnActionHandler>> dic_ButtonClick = new Dictionary<string, List<OnActionHandler>>();
}