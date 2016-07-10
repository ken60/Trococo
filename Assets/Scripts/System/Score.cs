using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField]
    private float m_GameOverWait;

    private Text m_ScoreText;
    private float m_TimeCount = 0.0f;

    void Start()
    {
        m_ScoreText = GetComponent<Text>();
    }

    void Update()
    {
        m_ScoreText.text = "スコア " + GameData.Instance.Score;

        if (GameData.Instance.IsGameOver)
        {
            m_TimeCount += Time.deltaTime;
            if (m_TimeCount >= m_GameOverWait)
            {
                m_TimeCount = 0.0f;
                //MoveOut();
                Destroy(gameObject);
            }
        }
    }

    //iTween
    void MoveOut()
    {
        Hashtable parameters = new Hashtable();
        parameters.Add("y", 100);
        parameters.Add("easeType", iTween.EaseType.easeInOutBack);
        parameters.Add("oncomplete", "DestroyText");
        parameters.Add("oncompletetarget", gameObject);
        iTween.MoveTo(gameObject, parameters);
    }

    void DestroyText()
    {
        Destroy(gameObject);
    }
}