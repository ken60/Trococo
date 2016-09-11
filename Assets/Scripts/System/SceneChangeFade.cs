using UnityEngine;
using UnityEngine.UI;

public class SceneChangeFade : MonoBehaviour
{
    [Range(0.0f, 1.0f), SerializeField]
    private float m_Alpha = 0.0f;
    [SerializeField]
    private float m_WaitTime = 0.0f;

    private Color m_Color;
    private Image m_Image;
    
    public bool m_isFadeIn = false;
    public bool m_isFadeOut = false;

    void Start()
    {
        m_Image = GetComponent<Image>();
        m_Color = m_Image.color;
    }

    void Update()
    {
        m_Color.a = m_Alpha;
        m_Image.color = m_Color;
    }

    public void FadeIn()
    {
        if (m_Alpha <= 1.0f)
        {
            m_isFadeIn = true;
            m_Alpha += 2 * Time.deltaTime;
        }
        else
        {
            m_isFadeIn = false;
        }
    }

    public void FadeOut()
    {
        if (0.0f <= m_Alpha)
        {
            m_isFadeOut = true;
            m_Alpha -= 2 * Time.deltaTime;
        }
        else
        {
            m_isFadeOut = false;
        }
    }

    public bool fadeIn
    {
        get { return m_isFadeIn; }
        set { m_isFadeIn = value; }
    }

    public bool fadeOut
    {
        get { return m_isFadeOut; }
        set { m_isFadeOut = value; }
    }
}