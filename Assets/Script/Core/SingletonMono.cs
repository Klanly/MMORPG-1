// **********************************************************************
// Copyright (C) 2018 EastHill Group .Co .Ltd
//
// 文件名(File Name):             SingletonMono.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2018/12/18 19:18:26
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************

using UnityEngine;
using System.Collections;

public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{

    #region 单例
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                //instance = new NetWorkHttp();
                //继承自MonoBehaviour的单例写法
                GameObject obj = new GameObject(typeof(T).Name);
                //不释放/不销毁这个物体
                DontDestroyOnLoad(obj);
                instance = obj.GetOrCreatComponent<T>();
            }
            return instance;
        }
    }
    #endregion


    void Awake()
    {
        OnAwake();
    }

    // Use this for initialization
    void Start()
    {
        OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
    }

    void Destroy()
    {
        BeforeOnDestroy();
    }

    protected virtual void OnAwake() { }
    protected virtual void OnStart() { }
    protected virtual void OnUpdate() { }
    protected virtual void BeforeOnDestroy() { }
}
