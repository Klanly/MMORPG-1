// **********************************************************************
// Copyright (C) 2018 The company name
//
// 文件名(File Name):             ProductDBModel.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2018/11/29 14:26:45
// 修改者列表(modifier):
// 模块描述(Module description):  通过此类来读取数据**工具生成，请勿修改
// **********************************************************************

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
/// <summary>
/// 商品数据管理类
/// </summary>
public partial class ProductDBModel : AbstractDBModel<ProductDBModel, ProductEntity>
{

    //返回所读取表的名称
    protected override string FileName{get { return "Product.data"; } }

    protected override ProductEntity MakeEntity(GameDataTableParser parse)
    {
        ProductEntity entity = new ProductEntity();

        //读取数据
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.Name = parse.GetFieldValue("Name");
        entity.Price = parse.GetFieldValue("Price").ToFloat();
        entity.PicName = parse.GetFieldValue("PicName");
        entity.Desc = parse.GetFieldValue("Desc");

        return entity;
    }
}
