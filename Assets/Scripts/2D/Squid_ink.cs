using UnityEngine;
using UnityEngine.UI;

public class Squid_ink : MonoBehaviour
{
    private Image m_Image;
    private Color m_Color;
    private float m_Alpha = 1.0f;

    void Start()
    {
        m_Color = GetComponent<Image>().color;
        m_Image = GetComponent<Image>();
    }
    void Update()
    {
        m_Alpha -= 0.05f * Time.deltaTime;

        if (m_Alpha <= 0.0f)
            Destroy(gameObject);
        m_Image.color = new Color(m_Color.r, m_Color.g, m_Color.b, m_Alpha);

    }
}