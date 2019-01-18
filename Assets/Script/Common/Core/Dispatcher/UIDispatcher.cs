// **********************************************************************
// Copyright (C) 2018 EastHill Group .Co .Ltd
//
// 文件名(File Name):             UIDispatcher.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2019-01-18 10:49:46
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIDispatcher : DispatcherBase<UIDispatcher>
{
    //按钮点击事件委托原型
    public delegate void OnButtonClickHandler(params System.Object[] param);
    //定义字典并实例化
    private Dictionary<string, List<OnButtonClickHandler>> dic_ButtonClick = new Dictionary<string, List<OnButtonClickHandler>>();

    /// <summary>
    /// 添加按钮点击监听
    /// </summary>
    /// <param name="btnKey"></param>
    /// <param name="handler"></param>
    public void AddBtnEventListener(string btnKey, OnButtonClickHandler handler)
    {
        if (dic_ButtonClick.ContainsKey(btnKey))
        {
            //存在actionID即list中有数据
            dic_ButtonClick[btnKey].Add(handler);
        }
        else
        {
            //若没有actionID,则说明其中没有这个list，因此需要实例化list
            List<OnButtonClickHandler> lstHandler = new List<OnButtonClickHandler>();
            lstHandler.Add(handler);
            dic_ButtonClick[btnKey] = lstHandler;
        }
    }

    /// <summary>
    /// 移除按钮点击监听
    /// </summary>
    /// <param name="btnKey"></param>
    /// <param name="handler"></param>
    public void RemoveBtnEventListener(string btnKey, OnButtonClickHandler handler)
    {
        if (dic_ButtonClick.ContainsKey(btnKey))
        {
            //取出list
            List<OnButtonClickHandler> lstHandler = dic_ButtonClick[btnKey];
            lstHandler.Remove(handler);
            //列表为0时
            if (lstHandler.Count == 0)
            {
                //从字典中移除key
                dic_ButtonClick.Remove(btnKey);
            }
        }
    }

    /// <summary>
    /// 派发按钮点击
    /// </summary>
    /// <param name="btnKey"></param>
    /// <param name="param"></param>
    public void DispatchBtn(string btnKey, params System.Object[] param)
    {
        if (dic_ButtonClick.ContainsKey(btnKey))
        {
            //如果有此ID，取到这个集合
            List<OnButtonClickHandler> lstHandler = dic_ButtonClick[btnKey];
            if (lstHandler != null && lstHandler.Count > 0)
            {
                for (int i = 0; i < lstHandler.Count; i++)
                {
                    if (lstHandler[i] != null)
                    {
                        //执行
                        lstHandler[i](param);
                    }
                }
            }
        }
    }
}
