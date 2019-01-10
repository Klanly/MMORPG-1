// **********************************************************************
// Copyright (C) 2018 EastHill Group .Co .Ltd
//
// 文件名(File Name):             UISceneLogOnView.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2019-01-09 10:14:27
// 修改者列表(modifier):
// 模块描述(Module description):登录场景UI视图（View）
// **********************************************************************

using UnityEngine;
using System.Collections;

public class UISceneLogOnView : UISceneViewBase
{
    protected override void OnStart()
    {
        base.OnStart();

        StartCoroutine(OpenLogOnWindow());
    }

    private IEnumerator OpenLogOnWindow()
    {
        yield return new WaitForSeconds(.2f);

        //没必要获取gameobject，只需要打开即可
        AccountCtrl.Instance.OpenLogOnView();
        //GameObject obj = WindowUIMgr.Instance.OpenWindow(WindowUIType.LogOn);
    }
}
