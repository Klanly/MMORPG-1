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

public class DispatcherBase<T, P, X> : IDisposable
    where T : new()
    where P : class
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
    public delegate void OnActionHandler(P p);
    //定义字典并实例化
    public Dictionary<X, List<OnActionHandler>> dic = new Dictionary<X, List<OnActionHandler>>();

    #region AddEventListener 添加监听
    /// <summary>
    /// 添加监听
    /// </summary>
    /// <param name="key"></param>
    /// <param name="handler"></param>
    public void AddEventListener(X key, OnActionHandler handler)
    {
        if (dic.ContainsKey(key))
        {
            //存在actionID即list中有数据
            dic[key].Add(handler);
        }
        else
        {
            //若没有actionID,则说明其中没有这个list，因此需要实例化list
            List<OnActionHandler> lstHandler = new List<OnActionHandler>();
            lstHandler.Add(handler);
            dic[key] = lstHandler;
        }
    }
    #endregion

    #region RemoveEventListener 移除监听
    /// <summary>
    /// 移除监听
    /// </summary>
    /// <param name="key"></param>
    /// <param name="handler"></param>
    public void RemoveEventListener(X key, OnActionHandler handler)
    {
        if (dic.ContainsKey(key))
        {
            //取出list
            List<OnActionHandler> lstHandler = dic[key];
            lstHandler.Remove(handler);
            //列表为0时
            if (lstHandler.Count == 0)
            {
                //从字典中移除key
                dic.Remove(key);
            }
        }
    }
    #endregion

    #region Dispatch 派发
    /// <summary>
    /// 派发
    /// </summary>
    /// <param name="key"></param>
    /// <param name="p"></param>
    public void Dispatch(X key, P p)
    {
        if (dic.ContainsKey(key))
        {
            //如果有此ID，取到这个集合
            List<OnActionHandler> lstHandler = dic[key];
            if (lstHandler != null && lstHandler.Count > 0)
            {
                for (int i = 0; i < lstHandler.Count; i++)
                {
                    if (lstHandler[i] != null)
                    {
                        //执行
                        lstHandler[i](p);
                    }
                }
            }
        }
    }

    //有些派发只有key，因此需要重载
    public void Dispatch(X key)
    {
        Dispatch(key, null);
    }
    #endregion
}