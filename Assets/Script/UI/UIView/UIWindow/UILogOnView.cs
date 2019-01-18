// **********************************************************************
// Copyright (C) 2018 EastHill Group .Co .Ltd
//
// 文件名(File Name):             UILogOnView.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2019-01-09 11:57:23
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************

using UnityEngine;
using System.Collections;

public class UILogOnView : UIWindowViewBase
{

    protected override void OnBtnClick(GameObject go)
    {
        base.OnBtnClick(go);
        switch (go.name)
        {
            case "btnLogOn":
                UIDispatcher.Instance.DispatchBtn("UILogOnView_btnLogOn");
                break;
            case "btnToReg":
                UIDispatcher.Instance.DispatchBtn("UILogOnView_btnToReg");
                break;
        }
    }
}
