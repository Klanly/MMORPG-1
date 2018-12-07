// **********************************************************************
// Copyright (C) 2018 The company name
//
// 文件名(File Name):             TestMMOMemory.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2018/11/29 11:22:43
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using LitJson;

public class TestMMOMemory : MonoBehaviour
{


    // Use this for initialization
    void Start()
    {
        ////前端调用时，需要先判断isBusy,不繁忙时则请求数据
        //if (!NetWorkHttp.Instance.IsBusy)
        //{
        //    NetWorkHttp.Instance.SendData(GlobalInit.WebAccountUrl + "api/account?id=1", GetCallBack);
        //} 

        //post
        if (!NetWorkHttp.Instance.IsBusy)
        {
            //以下即为FormBody
            //注册一般传用户名和密码，但目前暂时没有用户名和密码，先传个空值
            JsonData jsonData = new JsonData();
            jsonData["UserName"] = "";
            jsonData["Pwd"] = "";

            NetWorkHttp.Instance.SendData(GlobalInit.WebAccountUrl + "api/account", PostCallBack, isPost: true, json:jsonData.ToJson());
        }
    }

    private void GetCallBack(NetWorkHttp.CallBackArgs obj)
    {
        if (obj.HasError)
        {
            Debug.Log(obj.ErrorMsg);
        }
        else
        {
            AccountEntity entity = LitJson.JsonMapper.ToObject<AccountEntity>(obj.Json);
            Debug.Log(entity.UserName);
        }
    }

    /// <summary>
    /// PostCallBack回调
    /// </summary>
    /// <param name="obj"></param>
    private void PostCallBack(NetWorkHttp.CallBackArgs obj)
    {
        if (obj.HasError)
        {
            Debug.Log(obj.ErrorMsg);
        }
        else
        {

        }
    }


}

