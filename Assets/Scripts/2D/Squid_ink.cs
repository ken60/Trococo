﻿using UnityEngine;
using UnityEngine.UI;

public class Squid_ink : MonoBehaviour
{
    [SerializeField]
    private float m_AliveTime = 1f;

    private Image m_Image;
    private Color m_Color;
    private float m_Alpha = 1.0f;

    void Start()
    {
        m_Image = GetComponent<Image>();
        m_Color = m_Image.color;
    }
    void Update()
    {
        m_Alpha -= 1.0f / m_AliveTime * Time.deltaTime;

        if (m_Alpha <= 0.0f)
            Destroy(gameObject);
        m_Image.color = new Color(m_Color.r, m_Color.g, m_Color.b, m_Alpha);

    }
}