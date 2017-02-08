using UnityEngine;
using System.Collections;

public class LightManager : SingletonMonoBehaviour<LightManager>
{
    [SerializeField]
    private Light[] m_Light;

    void Start()
    {

    }

    public void ShadowEnabled(bool f)
    {
        if (f)
        {
            foreach (Light l in m_Light)
            {
                l.shadows = LightShadows.Soft;
            }
        }
        else
        {
            foreach (Light l in m_Light)
            {
                l.shadows = LightShadows.None;
            }
        }
    }

}
