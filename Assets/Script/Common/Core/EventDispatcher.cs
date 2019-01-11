// **********************************************************************
// Copyright (C) 2018 EastHill Group .Co .Ltd
//
// 文件名(File Name):             PengYouQuan.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2018/12/13 14:39:38
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 观察者模式
/// </summary>
public class EventDispatcher :Singleton<EventDispatcher>
{
    //定义委托原型；参数数组
    public delegate void OnActionHandler(byte[] buffer);

    private Dictionary<ushort, List<OnActionHandler>> dic = new Dictionary<ushort, List<OnActionHandler>>();

    /// <summary>
    /// 添加监听
    /// </summary>
    /// <param name="protoCode"></param>
    /// <param name="handler"></param>
    public void AddEventListener(ushort protoCode,OnActionHandler handler)
    {        
        if (dic.ContainsKey(protoCode))
        {
            //存在actionID即list中有数据
            dic[protoCode].Add(handler);
        }
        else
        {
            //若没有actionID,则说明其中没有这个list，因此需要实例化list
            List<OnActionHandler> lstHandler = new List<OnActionHandler>();
            lstHandler.Add(handler);
            dic[protoCode] = lstHandler;
        }
    }

    /// <summary>
    /// 移除监听
    /// </summary>
    /// <param name="protoCode"></param>
    /// <param name="handler"></param>
    public void RemoveEventListener(ushort protoCode, OnActionHandler handler)
    {
        if (dic.ContainsKey(protoCode))
        {
            //取出list
            List<OnActionHandler> lstHandler = dic[protoCode];
            lstHandler.Remove(handler);
            //列表为0时
            if (lstHandler.Count == 0)
            {
                //从字典中移除key
                dic.Remove(protoCode);
            }
        }
    }

    /// <summary>
    /// 派发消息，派发给监听此ID的人
    /// </summary>
    /// <param name="protoCode"></param>
    /// <param name="param"></param>
    public void Dispatch(ushort protoCode, byte[] buffer)
    {
        if (dic.ContainsKey(protoCode))
        {
            //如果有此ID，取到这个集合
            List<OnActionHandler> lstHandler = dic[protoCode];
            if (lstHandler != null && lstHandler.Count > 0)
            {
                for(int i = 0; i < lstHandler.Count; i++)
                {
                    if (lstHandler[i] != null)
                    {
                        //执行
                        lstHandler[i](buffer);
                    }
                }
            }
        }
    }

    //================================================================
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
