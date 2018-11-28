#region 模块信息
// **********************************************************************
// Copyright (C) 2018 The company name
//
// 文件名(File Name):             TestStream.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2018/11/28 17:38:4
// 修改者列表(modifier):
// 模块描述(Module description):测试数据类型转化为字节
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;
using System;

public class TestStream : MonoBehaviour {

    
	// Use this for initialization
	void Start () {
        //int a = 98598998;

        //byte[] arr = BitConverter.GetBytes(a);

        //for(int i = 0; i < arr.Length; i++)
        //{
        //    Debug.Log(string.Format("arr{0}={1}", i, arr[i]));
        //}

        //byte[] arr = new byte[4];
        //arr[0] = 86;
        //arr[1] = 128;
        //arr[2] = 224;
        //arr[3] = 5;
        byte[] arr = { 86, 128, 224, 5};

        int a = BitConverter.ToInt32(arr, 0);
        Debug.Log("a="+a);

    }
	

}
