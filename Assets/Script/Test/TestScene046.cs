using UnityEngine;
using System.Collections;

public class TestScene046 : MonoBehaviour
{

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
    private string m_Test = "AAA";
#elif UNITY_ANDROID
    private string m_Test = "BBB";
#elif UNITY_IPHONE
    private string m_Test = "AAA";
#endif

    void Start()
    {
        Debug.Log(m_Test);
#if DEBUG_LOG
        Debug.Log(m_Test);
#endif

#if DEBUG_MODEL
        Debug.Log("Debug Model");
#endif
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            SceneMgr.Instance.LoadToCity();
        }
    }
}
