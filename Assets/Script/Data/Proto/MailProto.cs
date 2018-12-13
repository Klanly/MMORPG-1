// **********************************************************************
// Copyright (C) 2018 EastHill Group .Co .Ltd
//
// 文件名(File Name):             MailProto.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2018/12/13 14:0:8
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************

using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 发送邮件
/// </summary>
public struct MailProto : IProto
{
    public ushort ProtoCode { get { return 1001; } }

    public int Count; //元宝数量
    public bool IsSuccess; //是否成功
    public string SuccMsg; //成功提示
    public string SuccCode; //成功编码
    public int ErrorCode; //错误编码
    public int ItemCount; //道具数量
    public List<int> ItemIdList; //道具编号
    public List<int> ItemNameList; //道具名称



    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(Count);
            ms.WriteBool(IsSuccess);
            if (IsSuccess)
            {
                ms.WriteUTF8String(SuccMsg);
                ms.WriteUTF8String(SuccCode);
            }
            else
            {
                ms.WriteInt(ErrorCode);
            }
            ms.WriteInt(ItemCount);
            for (int i = 0; i < ItemCount; i++)
            {
                ms.WriteInt(ItemIdList[i]);
                ms.WriteInt(ItemNameList[i]);
            }
            return ms.ToArray();
        }
    }

    public static MailProto GetProto(byte[] buffer)
    {
        MailProto proto = new MailProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.Count = ms.ReadInt();
            proto.IsSuccess = ms.ReadBool();
            if (proto.IsSuccess)
            {
                proto.SuccMsg = ms.ReadUTF8String();
                proto.SuccCode = ms.ReadUTF8String();
            }
            else
            {
                proto.ErrorCode = ms.ReadInt();
            }
            proto.ItemCount = ms.ReadInt();
            proto.ItemIdList = new List<int>();
            proto.ItemNameList = new List<int>();
            for (int i = 0; i < proto.ItemCount; i++)
            {
                int _ItemId = ms.ReadInt();  //道具编号
                proto.ItemIdList.Add(_ItemId);
                int _ItemName = ms.ReadInt();  //道具名称
                proto.ItemNameList.Add(_ItemName);
            }

        }
        return proto;
    }
}