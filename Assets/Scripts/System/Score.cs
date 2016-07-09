using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField]
    private Transform m_Player;
    [SerializeField]
    private int m_SiblingIndex = 0;

    private Text m_ScoreText;
    private int m_Score;

    private void Start()
    {
        transform.SetSiblingIndex(m_SiblingIndex);
        m_ScoreText = GetComponent<Text>();
        m_Score = 0;
    }

    private void Update()
    {
        m_Score = (int)m_Player.position.z;
        m_ScoreText.text = "Score: " + m_Score;
    }

    public int GetScore()
    {
        return m_Score;
    }
}