using UnityEngine;

public class Panel_Gashapon : MonoBehaviour
{
    [SerializeField]
    private int m_GashaponPrice = 100;

    void Start()
    {
        transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
    }

    void Update()
    {

    }

    public void Button_Gashapon()
    {
        if (GameManager.Instance.totalGoldCoinNum >= m_GashaponPrice)
        {
            int charId = Random.Range(0, GameManager.Instance.totalCharacterNumber);
            //コインを減らす
            GameManager.Instance.totalGoldCoinNum -= m_GashaponPrice;
            //キャラクターを開放
            GameManager.Instance.OpenCharacter(charId);
            //セーブ
            GameManager.Instance.SaveGame();

            print(charId);
        }
        else
        {
            Debug.Log("コインが足りません");
        }
    }
}