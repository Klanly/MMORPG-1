//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-04 22:26:39
//备    注：
//===================================================
using UnityEngine;
using System.Collections;

/// <summary>
/// UI层级管理器
/// </summary>
public class LayerUIMgr : Singleton<LayerUIMgr> 
{
    /// <summary>
    /// UI层级深度
    /// </summary>
    private int m_UIViewLayer = 50;

    /// <summary>
    /// 重置
    /// </summary>
    public void Reset()
    {
        m_UIViewLayer = 50;
    }

    /// <summary>
    /// 检查窗口数量 如果没有打开的窗口 重置
    /// </summary>
    public void CheckOpenWindow()
    {
        m_UIViewLayer--;
        if (UIViewUtil.Instance.OpenWindowCount == 0)
        {
            Reset();
        }
    }

    /// <summary>
    /// 设置层级
    /// </summary>
    public void SetLayer(GameObject obj)
    {
        m_UIViewLayer++;
        //获取canvas组件
        Canvas m_Canvas = obj.GetComponent<Canvas>();
        //设置sortingOrder（canvas层级）
        m_Canvas.sortingOrder = m_UIViewLayer;

        //UIPanel[] panArr = obj.GetComponentsInChildren<UIPanel>();

        //if (panArr.Length > 0)
        //{
        //    for (int i = 0; i < panArr.Length; i++)
        //    {
        //        panArr[i].depth += m_UIPanelDepth;
        //    }
        //}
    }
}