//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-05 11:13:45
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class SceneLoadingCtrl : MonoBehaviour 
{
    /// <summary>
    /// UI场景控制器
    /// </summary>
    [SerializeField]
    private UISceneLoadingCtrl m_UILoadingCtrl;

    private AsyncOperation m_Async = null;

    /// <summary>
    /// 当前的进度
    /// </summary>
    private int m_CurrProgress = 0;

	void Start ()
	{
        //监听场景是否加载完毕
        DelegateDefine.Instance.OnSceneLoadOk += OnSceneLoadOk;

        LayerUIMgr.Instance.Reset();
        StartCoroutine(LoadingScene());
    }

    void OnDestroy()
    {        
        //销毁时移除监听
        DelegateDefine.Instance.OnSceneLoadOk -= OnSceneLoadOk;
    }

    private void OnSceneLoadOk()
    {
        Debug.Log("销毁UI");

        if (m_UILoadingCtrl != null)
        {
            Destroy(m_UILoadingCtrl.gameObject);
        }
    }

    private IEnumerator LoadingScene()
    {
        string strSceneName = string.Empty;
        switch (SceneMgr.Instance.CurrentSceneType)
        {
            case SceneType.LogOn:
                strSceneName = "Scene_LogOn";
                break;
            case SceneType.City:
                strSceneName = "GameScene_CunZhuang";
                break;
            case SceneType.ShaMo:
                strSceneName = "GameScene_ShaMo";
                break;
        }

        m_Async = SceneManager.LoadSceneAsync(strSceneName,LoadSceneMode.Additive);
        m_Async.allowSceneActivation = false;
        yield return m_Async;
    }

	void Update ()
	{
        int toProgress = 0;

        if (m_Async.progress < 0.9f)
        {
            toProgress = Mathf.Clamp((int)m_Async.progress * 100, 1, 100);
        }
        else
        {
            toProgress = 100;
        }

        if (m_CurrProgress < toProgress)
        {
            m_CurrProgress++;
        }
        else
        {
            m_Async.allowSceneActivation = true;
        }

        m_UILoadingCtrl.SetProgressValue(m_CurrProgress * 0.01f);
    }
}