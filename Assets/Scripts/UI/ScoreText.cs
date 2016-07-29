using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    private Text m_ScoreText;

    void Start()
    {
        m_ScoreText = GetComponent<Text>();
    }

    void Update()
    {
        m_ScoreText.text = "キョリ " + GameManager.Instance.score + "m";
    }
}