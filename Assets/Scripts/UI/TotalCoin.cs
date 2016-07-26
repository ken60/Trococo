using UnityEngine;
using UnityEngine.UI;

public class TotalCoin : MonoBehaviour
{
    private Text m_Text;

	void Start ()
	{
        m_Text = GetComponent<Text>();

    }
	
	void Update ()
	{
        m_Text.text = "所持コイン " + GameManager.Instance.totalCoinNum + "枚 ";

    }
}