using UnityEngine;
using System.Collections;

public class TestScene046 : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            SceneMgr.Instance.LoadToCity();
        }
    }
}
