using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinText : MonoBehaviour
{
    [SerializeField]
    private float m_GameOverWait;

    private Text m_CoinText;
    private float m_TimeCount = 0.0f;

    void Start()
    {
        m_CoinText = GetComponent<Text>();
    }

    void Update()
    {
        m_CoinText.text = "コイン " + GameManager.Instance.Coin;

        if (GameSceneManager.Instance.IsGameOver)
        {
            m_TimeCount += Time.deltaTime;
            if (m_TimeCount >= m_GameOverWait)
            {
                m_TimeCount = 0.0f;
                Destroy(gameObject);
            }
        }
    }


    //iTween
    void MoveIn()
    {
        Hashtable parameters = new Hashtable();
        parameters.Add("y", -65);
        parameters.Add("easeType", iTween.EaseType.easeOutBounce);
        iTween.MoveTo(gameObject, parameters);
    }

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