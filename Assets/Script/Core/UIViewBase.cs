// **********************************************************************
// Copyright (C) 2018 EastHill Group .Co .Ltd
//
// 文件名(File Name):             UIViewBase.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2019-01-08 17:03:11
// 修改者列表(modifier):
// 模块描述(Module description): 所有UI的基类
// **********************************************************************

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIViewBase : MonoBehaviour
{
    void Awake()
    {
        OnAwake();
    }

    void Start()
    {
        //UIButton[] btnArr = GetComponentsInChildren<UIButton>(true);
        Button[] btnArr = GetComponentsInChildren<Button>(true);
        for (int i = 0; i < btnArr.Length; i++)
        {
            //UIEventListener.Get(btnArr[i].gameObject).onClick = BtnClick;
            EventTriggerListener.Get(btnArr[i].gameObject).onClick = BtnClick;
        }
        OnStart();
    }

    void OnDestroy()
    {
        BeforeOnDestroy();
    }

    private void BtnClick(GameObject go)
    {
        OnBtnClick(go);
    }

    protected virtual void OnAwake() { }
    protected virtual void OnStart() { }
    protected virtual void BeforeOnDestroy() { }
    protected virtual void OnBtnClick(GameObject go) { }
}
