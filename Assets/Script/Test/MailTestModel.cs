// **********************************************************************
// Copyright (C) 2018 EastHill Group .Co .Ltd
//
// 文件名(File Name):             MailTestModel.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2018/12/14 11:51:44
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************

using UnityEngine;
using System.Collections;
using System;

public class MailTestModel : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        SocketDispatcher.Instance.AddEventListener(ProtoCodeDef.Mail_Get_Detail, OnGetMail);
    }

    private void OnGetMail(byte[] buffer)
    {
        Mail_Get_DetailProto proto = Mail_Get_DetailProto.GetProto(buffer);
        Debug.Log("proto.Name=" + proto.Name);
    }

    void OnDestroy()
    {
        SocketDispatcher.Instance.RemoveEventListener(ProtoCodeDef.Mail_Get_Detail, OnGetMail);
    }

}
