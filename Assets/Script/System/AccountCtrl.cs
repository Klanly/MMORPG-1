// **********************************************************************
// Copyright (C) 2018 EastHill Group .Co .Ltd
//
// 文件名(File Name):             AccountCtrl.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2019-01-09 12:22:43
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************

using UnityEngine;
using System.Collections;

public class AccountCtrl : Singleton<AccountCtrl>
{
    public void OpenLogOnView()
    {
        WindowUIMgr.Instance.OpenWindow(WindowUIType.LogOn);
    }
}
