// **********************************************************************
// Copyright (C) 2018 The company name
//
// 文件名(File Name):             AbstractDBModel.cs
// 作者(Author):                  Caedmom
// 创建时间(CreateTime):          2018/11/29 16:10:51
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 数据管理基类
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="P"></typeparam>
public abstract class AbstractDBModel<T,P>
    //子类上需要知道子类的单例，同时子类需具有可操作的数据类型，因此基类上就要有相应的泛型
    //T表示子类类型，P表示子类操作的数据类型
    where T :class,new()//子类需要实例化一次
    where P: AbstractEntity//数据实体继承自AbstractEntity
{
    protected List<P> m_List;
    protected Dictionary<int, P> m_Dic;

    public AbstractDBModel()
    {
        //在构造函数中进行实例化
        m_List = new List<P>();
        m_Dic = new Dictionary<int, P>();

        LoadData();
    }

    #region 单例
    private static T _Instance;

    public static T Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new T();
                //_Instance.Load();
            }
            return _Instance;
        }
    }
    #endregion

    #region 需要子类实现的属性或方法
    /// <summary>
    /// 数据文件名称
    /// </summary>
    protected abstract string FileName { get; }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    //子类所需的属性、数据类型
    protected abstract P MakeEntity(GameDataTableParser parse);
    #endregion

    #region 加载数据 LoadData()
    /// <summary>
    /// 加载数据
    /// </summary>
    private void LoadData()
    {
        //传入data路径，将表格放到streamingassets下，游戏安装后将复制到游戏外的路径，便于以后热更新
        using (GameDataTableParser parse = new GameDataTableParser(string.Format(@"D:\Backup\Documents\GitHub\MMORPG\www\Data\{0}", FileName)))
        {
            while (!parse.Eof)
            {
                //循环读取同时进行实例化

                //创建实体（子类创建后传入）
                P p = MakeEntity(parse);

                m_List.Add(p);
                m_Dic[p.Id] = p;

                parse.Next();
            }
        }

    }
    #endregion

    #region 获取集合 GetList()
    /// <summary>
    /// 获取集合
    /// </summary>
    /// <returns></returns>
    //提供接口查数据
    public List<P> GetList()
    {
        return m_List;
    }
    #endregion

    #region 根据编号获取实体 Get(int id)
    /// <summary>
    /// 根据商品编号查询商品
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public P Get(int id)
    {
        //使用字典时，需要进行判断是否包含这个Key
        if (m_Dic.ContainsKey(id))
        {
            return m_Dic[id];
        }
        return null;
    }
    #endregion
}
