// **********************************************************************
// Copyright (C) 2018 The company name
//
// 文件名(File Name):             TestMMOMemory.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2018/11/29 11:22:43
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestMMOMemory : MonoBehaviour {


    // Use this for initialization
    void Start()
    {
        List<ProductEntity> lst = ProductDBModel.Instance.GetList();

        for (int i = 0; i < lst.Count; i++)
        {
            Debug.Log("name=" + lst[i].Name);
        }

        Debug.Log(lst.Count);
    }
	
}

