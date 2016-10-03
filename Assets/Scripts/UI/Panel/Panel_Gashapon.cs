using UnityEngine;

public class Panel_Gashapon : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_Dialog;
    [SerializeField]
    private int m_GashaponPrice = 100;

    private bool m_isClick = false;
    private float m_TimeCount = 1.0f;

    void Start()
    {
        transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
    }

    void Update()
    {
        if (m_isClick)
        {
            m_TimeCount -= Time.deltaTime;
            if (m_TimeCount < 0.0f)
            {
                m_isClick = false;
                m_TimeCount = 1.0f;
            }
        }
    }

    public void Button_Gashapon()
    {
        if (m_isClick) return;
        m_isClick = true;

        int charId = 0;
        if (m_GashaponPrice <= GameDataManager.Instance.totalGoldCoinNum)
        {
            charId = Random.Range(0, GameDataManager.Instance.totalCharacterNumber);
            //コインを減らす
            GameDataManager.Instance.totalGoldCoinNum -= m_GashaponPrice;
            //キャラクターを開放
            GameDataManager.Instance.OpenCharacter(charId);
            //セーブ
            GameDataManager.Instance.SaveGame();

            GameObject dialog = Instantiate(m_Dialog[1], Vector3.zero, Quaternion.identity) as GameObject;
            dialog.GetComponent<DialogBox_CharOpen>().SetData(charId);
            dialog.transform.SetParent(GameObject.Find("Canvas").transform, false);
        }
        else
        {
            GameObject dialog = Instantiate(m_Dialog[0], Vector3.zero, Quaternion.identity) as GameObject;
            dialog.transform.SetParent(GameObject.Find("Canvas").transform, false);
            dialog.GetComponent<DialogBox>().SetText("コインが足りません");
        }
    }
}