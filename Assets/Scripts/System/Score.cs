using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Transform m_Player;

    [SerializeField]
    private int m_SiblingIndex = 0;

    private Text m_ScoreText;

    private void Start()
    {
        transform.SetSiblingIndex(m_SiblingIndex);
        m_ScoreText = GetComponent<Text>();
    }

    private void Update()
    {
        m_ScoreText.text = "Score: " + (int)m_Player.position.z;
    }
}