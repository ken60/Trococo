using UnityEngine;
using UnityEngine.UI;

public class SceneChangeFade : MonoBehaviour
{
    [Range(0.0f, 1.0f), SerializeField]
    private float m_Alpha;

    private Color m_Color;
    private Image m_Image;

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


    public void FadeOut()
    {
        if (0.0f < m_Alpha)
        {
            m_Alpha -= Time.deltaTime;
        }
    }

    public void FadeIn()
    {
        if (m_Alpha < 1.0f)
        {
            m_Alpha += Time.deltaTime;
        }
    }
}