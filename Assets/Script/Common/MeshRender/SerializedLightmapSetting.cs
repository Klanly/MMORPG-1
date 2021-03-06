using UnityEngine;
[ExecuteInEditMode]
public class SerializedLightmapSetting : MonoBehaviour
{
    public Texture2D[] lightmapFar, lightmapNear;
    public LightmapsMode mode;
#if UNITY_EDITOR
    public void OnEnable()
    {
        //烘焙完毕
        UnityEditor.Lightmapping.completed += LoadLightmaps;
    }
    public void OnDisable()
    {
        UnityEditor.Lightmapping.completed -= LoadLightmaps;
    }
#endif
    public void Start()
    {
        if (Application.isPlaying)
        {
            LightmapSettings.lightmapsMode = mode;
            int l1 = (lightmapFar == null) ? 0 : lightmapFar.Length;
            int l2 = (lightmapNear == null) ? 0 : lightmapNear.Length;
            int l = (l1 < l2) ? l2 : l1;
            LightmapData[] lightmaps = null;
            if (l > 0)
            {
                lightmaps = new LightmapData[l];
                for (int i = 0; i < l; i++)
                {
                    lightmaps[i] = new LightmapData();
                    if (i < l1)
                        lightmaps[i].lightmapFar = lightmapFar[i];
                    if (i < l2)
                        lightmaps[i].lightmapNear = lightmapNear[i];
                }
            }
            //将贴图指定到LightmapSettings
            LightmapSettings.lightmaps = lightmaps;
            Destroy(this);
        }
    }

    //去掉贴图的引用
    void OnDestroy()
    {
        if (lightmapFar != null && lightmapFar.Length > 0)
        {
            for (int i = 0; i < lightmapFar.Length; i++)
            {
                lightmapFar[i] = null;
            }
        }

        if (lightmapNear != null && lightmapNear.Length > 0)
        {
            for (int i = 0; i < lightmapNear.Length; i++)
            {
                lightmapNear[i] = null;
            }
        }
    }

#if UNITY_EDITOR
    public void LoadLightmaps()
    {
        mode = LightmapSettings.lightmapsMode;
        lightmapFar = null;
        lightmapNear = null;
        if (LightmapSettings.lightmaps != null && LightmapSettings.lightmaps.Length > 0)
        {
            int l = LightmapSettings.lightmaps.Length;
            lightmapFar = new Texture2D[l];
            lightmapNear = new Texture2D[l];
            for (int i = 0; i < l; i++)
            {
                lightmapFar[i] = LightmapSettings.lightmaps[i].lightmapFar;
                lightmapNear[i] = LightmapSettings.lightmaps[i].lightmapNear;
            }
        }
        MeshLightmapSetting[] savers = FindObjectsOfType<MeshLightmapSetting>();
        foreach (MeshLightmapSetting s in savers)
        {
            s.SaveSettings();
        }
    }
#endif
}