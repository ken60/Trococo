using UnityEngine;
using UnityEngine.UI;

public class SceneChangeFade : MonoBehaviour
{
    [Range(0.0f, 1.0f), SerializeField]
    private float m_Alpha;

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
        if (m_isFadeIn)
        {
            if (m_Alpha < 1.0f)
            {
                m_Alpha += Time.deltaTime * 2;
            }
            else
            {
                m_isFadeIn = false;
            }
        }

        if (m_isFadeOut)
        {
            if (0.0f < m_Alpha)
            {
                m_Alpha -= Time.deltaTime * 2;
            }
            {
                m_isFadeOut = false;
            }
        }

        m_Color.a = m_Alpha;
        m_Image.color = m_Color;
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