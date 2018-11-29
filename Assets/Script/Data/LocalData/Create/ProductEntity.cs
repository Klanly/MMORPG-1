// **********************************************************************
// Copyright (C) 2018 The company name
//
// 文件名(File Name):             ProductEntity.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2018/11/29 14:17:36
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************

using UnityEngine;
using System.Collections;

/// <summary>
/// 商品实体
/// </summary>
public partial class ProductEntity : AbstractEntity
{
    
    /// <summary>
    /// 商品名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 商品价格
    /// </summary>
    public float Price { get; set; }
    /// <summary>
    /// 图片名称
    /// </summary>
    public string PicName { get; set; }
    /// <summary>
    /// 描述
    /// </summary>
    public string Desc { get; set; }



}
