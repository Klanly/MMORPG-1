// **********************************************************************
// Copyright (C) 2018 EastHill Group .Co .Ltd
//
// 文件名(File Name):             TestScene048.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2018/12/24 12:6:32
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************

using UnityEngine;
using System.Collections;

public class TestScene048 : MonoBehaviour
{

    void Start()
    {
        Debug.Log("dataPath=" + Application.dataPath);
        Debug.Log("persistentDataPath=" + Application.persistentDataPath);
        Debug.Log("streamingAssetsPath=" + Application.streamingAssetsPath);
        Debug.Log("temporaryCachePath=" + Application.temporaryCachePath);

    }

    void Update()
    {

    }
}
