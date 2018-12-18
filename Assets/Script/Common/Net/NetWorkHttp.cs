// **********************************************************************
// Copyright (C) 2018 The company name
//
// 文件名(File Name):             NetWorkHttp.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2018/12/5 19:18:28
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************

using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Http通讯管理
/// </summary>
public class NetWorkHttp : SingletonMono<NetWorkHttp>
{

    #region 属性
    /// <summary>
    /// Web请求回调
    /// </summary>
    private Action<CallBackArgs> m_CallBack;
    /// <summary>
    /// Web请求回调数据
    /// </summary>
    private CallBackArgs m_CallBackArgs;

    /// <summary>
    /// 是否繁忙
    /// </summary>
    //添加一个参数（在本类中可读可写）；当我们在web请求时，比如前端有一个按钮，当点击该按钮两次或多次时，不应该总是发送请求。
    private bool m_IsBusy = false;
    /// <summary>
    /// 定义属性
    /// </summary>
    //外部访问，但不可写
    public bool IsBusy
    {
        get { return m_IsBusy; }
    }
    #endregion

    protected override void OnStart()
    {
        base.OnStart();
        //只在一个地方实例化，节省内存
        m_CallBackArgs = new CallBackArgs();
    }

    #region SendData() 发送Web数据
    /// <summary>
    /// 发送Web数据
    /// </summary>
    /// <param name="url"></param>
    /// <param name="callBack"></param>
    /// <param name="isPost"></param>
    /// <param name="json"></param>
    //如果前端/别的代码要访问这个类，则需要调用方法
    //因为与服务器通讯都是要获取请求的，所以我们使用SendData/SendMessage
    //声明委托后，加入委托参数
    public void SendData(string url, Action<CallBackArgs> callBack, bool isPost = false, string json = "")
    {
        //IsBusy在发送数据是这样判断
        //如果繁忙则立即return
        if (m_IsBusy) return;
        //如果不繁忙进入发送数据后，将标志位设为true--繁忙
        m_IsBusy = true;

        //委托，当有回调数据时即可执行委托
        m_CallBack = callBack;

        //判断，如果不是post请求，则调用Get
        if (!isPost)
        {
            GetUrl(url);
        }
        else
        {
            PostUrl(url, json);
        }
    }
    #endregion

    #region GetURL Get请求
    /// <summary>
    /// Get请求
    /// </summary>
    /// <param name="url"></param>
    //传入参数为url;由于传输的是JSON数据，post函数后还需要加关于json的参数
    private void GetUrl(string url)
    {
        WWW data = new WWW(url);//get使用WWW
        //启动协程
        StartCoroutine(Request(data));
    }
    #endregion

    #region PostUrl Post请求
    /// <summary>
    /// Post请求
    /// </summary>
    /// <param name="url"></param>
    /// <param name="json"></param>
    private void PostUrl(string url, string json)
    {
        //定义一个表单
        WWWForm form = new WWWForm();
        //给表单添加值(json，在表单中定义一个变量/字段jsonData,值即是json)，然后把表单提交给服务器，这就是Post请求
        form.AddField("", json);

        //使用WWW发送请求
        WWW data = new WWW(url, form);
        StartCoroutine(Request(data));

    }
    #endregion

    #region Request 请求服务器
    /// <summary>
    /// 请求服务器
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    //协程
    private IEnumerator Request(WWW data)
    {
        yield return data;

        //当返回数据（进行回调）时，设为IsBusy标志位为false
        m_IsBusy = false;

        //data无误则打印data，否则打印错误信息
        if (string.IsNullOrEmpty(data.error))
        {
            if (data.text == "null")
            {
                if (m_CallBack != null)
                {
                    //给参数赋值
                    m_CallBackArgs.HasError = true;
                    m_CallBackArgs.ErrorMsg = "未请求到数据";

                    //传入参数执行委托
                    m_CallBack(m_CallBackArgs);
                }
            }
            else
            {
                if (m_CallBack != null)
                {
                    //给参数赋值
                    m_CallBackArgs.HasError = false;
                    m_CallBackArgs.Json = data.text;

                    //传入参数执行委托
                    m_CallBack(m_CallBackArgs);
                }
            }

            //Debug.Log(data.text);
        }
        else
        {
            if (m_CallBack != null)
            {
                //给参数赋值
                m_CallBackArgs.HasError = true;
                m_CallBackArgs.ErrorMsg = data.error;

                //传入参数执行委托
                m_CallBack(m_CallBackArgs);
            }
            //Debug.Log(data.error);
        }
    }
    #endregion

    #region CallBackArgs Web请求回调数据
    //请求完毕后，需要有一个回调，可以用一个string类型的委托，但这样做不太好，新建一个类，再定义（CallBackArgs类型的）委托
    /// <summary>
    /// Web请求回调数据
    /// </summary>
    public class CallBackArgs : EventArgs
    {
        /// <summary>
        /// 是否有错
        /// </summary>
        public bool HasError;

        /// <summary>
        /// 错误原因
        /// </summary>
        public string ErrorMsg;

        /// <summary>
        /// Json数据
        /// </summary>
        public string Json;
    }
    #endregion
}