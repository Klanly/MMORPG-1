// **********************************************************************
// Copyright (C) 2018 The company name
//
// 文件名(File Name):             AccountEntity.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2018/12/7 13:59:14
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************

using System;

/// <summary>
/// 账户实体
/// </summary>
public class AccountEntity
{
    public int Id { get; set; }

    public string UserName { get; set; }

    public string Pwd { get; set; }

    public int YuanBao { get; set; }

    public int LastServerId { get; set; }

    public string LastServerName { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime UpdateTime { get; set; }

}
