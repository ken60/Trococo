using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinText : MonoBehaviour
{
    public Text m_GoldCoinText;
    public Text m_CopperCoinText;
    
    void Update()
    {
        m_GoldCoinText.text = GameManager.Instance.goldCoin.ToString();
        m_CopperCoinText.text = GameManager.Instance.copperCoin.ToString();
    }
}