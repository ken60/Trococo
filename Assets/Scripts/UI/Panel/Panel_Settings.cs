using UnityEngine;
using UnityEngine.UI;

public class Panel_Settings : MonoBehaviour
{
    [SerializeField]
    private Text[] m_ButtonText;
    [SerializeField]
    private Button[] m_Button;

    private Light m_Light;
    private bool m_AusioEnabled = false;
    private bool m_ShadowEnabled = false;


    void Start()
    {
        transform.localScale = Vector3.zero;

        m_AusioEnabled = GameManager.Instance.isAudioEnabled;
        m_ShadowEnabled = GameManager.Instance.isShadowEnable;

        if (m_AusioEnabled)
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

    public void Button_Audio()
    {
        m_AusioEnabled = !m_AusioEnabled;

        AudioManager.Instance.AudioMute(m_AusioEnabled);

        GameManager.Instance.isAudioEnabled = m_AusioEnabled;

        if (m_AusioEnabled)
            m_ButtonText[0].text = "ON";
        else
            m_ButtonText[0].text = "OFF";
    }

    public void Button_Shadow()
    {
        m_ShadowEnabled = !m_ShadowEnabled;

        LightManager.Instance.ShadowEnabled(m_ShadowEnabled);

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