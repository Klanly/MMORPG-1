// **********************************************************************
// Copyright (C) 2018 The company name
//
// 文件名(File Name):             NetWorkHttp.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2018/12/5 19:18:28
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************

using UnityEngine;
using System.Collections;

/// <summary>
/// Http通讯管理
/// </summary>
public class NetWorkHttp : MonoBehaviour {

    #region 单例
    private static NetWorkHttp instance;

    public static NetWorkHttp Instance
    {
        get
        {
            if(instance == null)
            {
                //instance = new NetWorkHttp();
                //继承自MonoBehaviour的单例写法
                GameObject obj = new GameObject("NetWorkHttp");
                //不释放/不销毁这个物体
                DontDestroyOnLoad(obj);
                instance = obj.GetOrCreatComponent<NetWorkHttp>();
            }
            return instance;
        }
    }
    #endregion

}
