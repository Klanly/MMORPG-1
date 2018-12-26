// **********************************************************************
// Copyright (C) 2018 EastHill Group .Co .Ltd
//
// 文件名(File Name):             AssetBundleLoaderAsync.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2018-12-26 14:31:28
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************

using UnityEngine;
using System.Collections;

public class AssetBundleLoaderAsync : MonoBehaviour
{
    private string m_FullPath;
    private string m_Name;

    private AssetBundleCreateRequest request;
    private AssetBundle bundle;

    //定义委托，接收ab包加载完毕的通知
    public System.Action<Object> OnLoadComplete;

    public void Init(string path, string name)
    {
        m_FullPath = LocalFileMgr.Instance.LocalFilePath + path;
        m_Name = name;
    }

    void Start()
    {
        StartCoroutine(Load());

    }

    private IEnumerator Load()
    {
        request = AssetBundle.LoadFromMemoryAsync(LocalFileMgr.Instance.GetBuffer(m_FullPath));
        yield return request;
        bundle = request.assetBundle;

        if (OnLoadComplete != null)
        {
            OnLoadComplete(bundle.LoadAsset(m_Name));
            //AssetBundleLoaderAsync.cs脚本每次请求时会创建一个物体，因此在请求结束后将自身销毁即可
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (bundle != null) bundle.Unload(false);
        m_FullPath = null;
        m_Name = null;
    }
}
