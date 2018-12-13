//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-01 22:26:02
//备    注：
//===================================================
using UnityEngine;
using System.Collections;

public class GlobalInit : MonoBehaviour 
{
    //定义委托
    public delegate void OnReceiveProtoHandler(ushort protoCode, byte[] buffer);
    public OnReceiveProtoHandler OnReceiveProto;

    #region 常量
    /// <summary>
    /// 昵称KEY
    /// </summary>
    public const string MMO_NICKNAME = "MMO_NICKNAME";

    /// <summary>
    /// 密码KEY
    /// </summary>
    public const string MMO_PWD = "MMO_PWD";

    /// <summary>
    /// 账户服务器地址
    /// </summary>
    public const string WebAccountUrl = "http://192.168.20.129:8080/";
    #endregion

    public static GlobalInit Instance;

    /// <summary>
    /// 玩家注册时候的昵称
    /// </summary>
    [HideInInspector]
    public string CurrRoleNickName;

    /// <summary>
    /// 当前玩家
    /// </summary>
    [HideInInspector]
    public RoleCtrl CurrPlayer;

    /// <summary>
    /// UI动画曲线
    /// </summary>
    public AnimationCurve UIAnimationCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

	void Start ()
	{
	
	}
}