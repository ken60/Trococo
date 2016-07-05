using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Transform m_Player;

    private Text m_ScoreText;

    private void Start()
    {
        m_ScoreText = GetComponent<Text>();
    }

    private void Update()
    {
        m_ScoreText.text = "Score: " + (int)m_Player.position.z;

    }
}