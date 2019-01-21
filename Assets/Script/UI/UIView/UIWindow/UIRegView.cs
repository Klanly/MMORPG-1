// **********************************************************************
// Copyright (C) 2018 EastHill Group .Co .Ltd
//
// 文件名(File Name):             UIRegView.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2019-01-09 11:59:19
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************

using UnityEngine;
using System.Collections;

public class UIRegView : UIWindowViewBase
{
    protected override void OnBtnClick(GameObject go)
    {
        base.OnBtnClick(go);
        switch (go.name)
        {
            case "btnReg":
                Debug.Log("点击了注册按钮");
                UIDispatcher.Instance.Dispatch(ConstDefine.UIRegView_btnReg);
                break;
            case "btnToLogOn":
                Debug.Log("点击了返回登录按钮");
                UIDispatcher.Instance.Dispatch(ConstDefine.UIRegView_btnToLogOn);
                break;
        }
    }
}
