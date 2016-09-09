using UnityEngine;
using UnityEngine.UI;

public class Panel_Settings : MonoBehaviour
{
    [SerializeField]
    private Text[] m_ButtonText;
    [SerializeField]
    private Button[] m_Button;
    [SerializeField]
    private Light m_Light;
    
    private bool m_Ausio = true;
    private bool m_ShadowEnabled = true;


    void Start()
    {
        transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);

        m_Ausio = GameManager.Instance.isAusioMute;
        m_ShadowEnabled = GameManager.Instance.isShadowEnable;

        if (m_Ausio)
            m_ButtonText[0].text = "ON";
        else
            m_ButtonText[0].text = "OFF";

        if (m_ShadowEnabled)
            m_ButtonText[1].text = "ON";
        else
            m_ButtonText[1].text = "OFF";
    }

    void Update()
    {

    }

    public void Button_Sound()
    {
        m_Ausio = !m_Ausio;
        AudioManager.Instance.AudioMute(!m_Ausio);

        if (m_Ausio)
            m_ButtonText[0].text = "ON";
        else
            m_ButtonText[0].text = "OFF";
    }

    public void Button_Shadow()
    {
        m_ShadowEnabled = !m_ShadowEnabled;
        if (m_ShadowEnabled)
        {
            //m_Light.shadows = LightShadows.Soft;
        }
        else
        {
            //m_Light.shadows = LightShadows.None;
        }

        GameManager.Instance.isShadowEnable = m_ShadowEnabled;

        if (m_ShadowEnabled)
            m_ButtonText[1].text = "ON";
        else
            m_ButtonText[1].text = "OFF";
    }

    public void Button_etc()
    {

    }

    public void Button_Credit()
    {
    }
}