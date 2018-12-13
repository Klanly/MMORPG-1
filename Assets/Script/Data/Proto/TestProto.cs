// **********************************************************************
// Copyright (C) 2018 The company name
//
// 文件名(File Name):             TestProto.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2018/12/12 9:50:0
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************

using UnityEngine;
using System.Collections;

/// <summary>
/// 测试协议
/// </summary>
public struct TestProto: IProto
{
    #region 属性
    /// <summary>
    /// 属性
    /// </summary>
    //协议编号
    public ushort ProtoCode { get { return 1004; } } 
    //协议数据
    public int Id;
    public string Name;
    public int Type;
    public float Price;
    #endregion

    public byte[] ToArray()
    {
        using(MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(Id);
            ms.WriteUTF8String(Name);
            ms.WriteInt(Type);
            ms.WriteFloat(Price);
            return ms.ToArray();
        }
    }

    public static TestProto GetProto(byte[] buffer)
    {
        TestProto proto = new TestProto();
        using(MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.Id = ms.ReadInt();
            proto.Name = ms.ReadUTF8String();
            proto.Type = ms.ReadInt();
            proto.Price = ms.ReadFloat();
        }
        return proto;
    }
}
