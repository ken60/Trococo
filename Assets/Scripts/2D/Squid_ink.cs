using UnityEngine;
using UnityEngine.UI;

public class Squid_ink : MonoBehaviour
{
    [SerializeField]
    private int m_SiblingIndex = 0;

    private Image m_Image;
    private Color m_Color;
    private float m_Alpha = 1.0f;
    private float m_Scale = 5.0f;
    private Scene m_Scene = 0;

    enum Scene
    {
        Fadein = 0,
        Fadeout,
    }

    void Start()
    {
        transform.SetSiblingIndex(m_SiblingIndex);
        m_Color = GetComponent<Image>().color;
        m_Image = GetComponent<Image>();
    }
    void Update()
    {
        switch (m_Scene)
        {
            case Scene.Fadein:
                //m_Scale -= 5.0f * Time.deltaTime;

                // if (m_Scale <= 1.1f) m_Scene++;

                //m_Image.rectTransform.localScale = new Vector3(m_Scale, m_Scale, m_Scale);
                m_Scene++;
                break;
            case Scene.Fadeout:
                m_Alpha -= 0.08f * Time.deltaTime;

                if (m_Alpha <= 0.0f)
                    Destroy(gameObject);
                m_Image.color = new Color(m_Color.r, m_Color.g, m_Color.b, m_Alpha);

                break;
        }
    }
}