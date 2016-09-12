using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinText : MonoBehaviour
{
    public Text m_GoldCoinText;
    
    void Update()
    {
        m_GoldCoinText.text = GameDataManager.Instance.goldCoin.ToString();
    }
}