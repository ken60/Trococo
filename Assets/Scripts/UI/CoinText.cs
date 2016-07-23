using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinText : MonoBehaviour
{
    private Text m_CoinText;

    void Start()
    {
        m_CoinText = GetComponent<Text>();
    }

    void Update()
    {
        m_CoinText.text = "コイン " + GameManager.Instance.coin;

    }
}