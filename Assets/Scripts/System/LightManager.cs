using UnityEngine;
using System.Collections;

public class LightManager : SingletonMonoBehaviour<LightManager>
{
    private Light m_Light;

	void Start ()
    {
        m_Light = GameObject.Find("Directional Light").GetComponent<Light>();
    }

    public void ShadowEnabled(bool f)
    {
        if (f)
        {
            m_Light.shadows = LightShadows.Soft;
        }
        else
        {
            m_Light.shadows = LightShadows.None;
        }
    }

}
