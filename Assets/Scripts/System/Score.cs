using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text m_ScoreText;

    private void Start()
    {
        m_ScoreText = GetComponent<Text>();
    }

    private void Update()
    {
        m_ScoreText.text = "スコア " + GameData.Instance.Score;
    }
}