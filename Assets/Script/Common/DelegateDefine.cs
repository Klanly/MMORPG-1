// **********************************************************************
// Copyright (C) 2018 EastHill Group .Co .Ltd
//
// 文件名(File Name):             DelegateDefine.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2018/12/20 11:0:9
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************

using UnityEngine;
using System.Collections;
using System;

public class DelegateDefine:Singleton<DelegateDefine>
{
    /// <summary>
    /// 场景加载完毕
    /// </summary>
    public Action OnSceneLoadOk;

}
