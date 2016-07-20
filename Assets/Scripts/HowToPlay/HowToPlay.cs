using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HowToPlay : MonoBehaviour
{
    [SerializeField]
    private Sprite[] m_Sprite;
    [SerializeField]
    private Image m_ExplanatoryImage;
    [SerializeField]
    private Text[] m_ButtonText;
    [SerializeField]
    private GameObject m_Panel_Title;

    private GameObject m_Canvas;
    private int m_MaxPageNum;
    private int m_PageNmmber = 1;

    void Start()
    {
        m_Canvas = GameObject.Find("Canvas");
        m_MaxPageNum = m_Sprite.Length;
        MoveIn();
    }

    void Update()
    {
        //1ページ目の時は戻るボタンを隠す
        if (m_PageNmmber == 1)
        {
            m_ButtonText[0].text = "もどる";
        }
        else
        {
            m_ButtonText[0].text = "まえへ";
        }

        //最後のページの時はボタンテキストを「もどる」に
        if (m_PageNmmber == m_MaxPageNum)
        {
            m_ButtonText[1].text = "もどる";
        }
        else
        {
            m_ButtonText[1].text = "つぎへ";
        }

        //ページ切り替え
        switch (m_PageNmmber)
        {
            case 1:
                m_ExplanatoryImage.sprite = m_Sprite[0];
                break;

            case 2:
                m_ExplanatoryImage.sprite = m_Sprite[1];
                break;

            case 3:
                m_ExplanatoryImage.sprite = m_Sprite[2];
                break;

            case 4:
                m_ExplanatoryImage.sprite = m_Sprite[3];
                break;

            case 5:
                m_ExplanatoryImage.sprite = m_Sprite[4];
                break;
        }
    }

    //「もどる」ボタンをおした時
    public void Button_Prev()
    {
        //ページ数が1ページ未満の時ページ送り
        if (1 < m_PageNmmber)
            m_PageNmmber--;

        if (m_ButtonText[0].text == "もどる")
        {
            MoveOut();
            GameObject panel = Instantiate(m_Panel_Title, new Vector3(0.0f, 1645.0f, 0.0f), Quaternion.identity) as GameObject;
            panel.transform.SetParent(m_Canvas.transform, false);
        }
    }

    //「つぎへ」ボタンをおした時
    public void Button_Next()
    {
        //ページ数が最大未満の時ページ送り
        if (m_PageNmmber < m_MaxPageNum)
            m_PageNmmber++;

        //最終ページの時はパネルを隠す
        if (m_ButtonText[1].text == "もどる")
        {
            MoveOut();
            GameObject panel = Instantiate(m_Panel_Title, new Vector3(0.0f, 1645.0f, 0.0f), Quaternion.identity) as GameObject;
            panel.transform.SetParent(m_Canvas.transform, false);
        }
    }

    //iTween
    void MoveIn()
    {
        Hashtable parameters = new Hashtable();
        parameters.Add("y", Screen.height * 0.5f);
        parameters.Add("easeType", iTween.EaseType.easeInOutBack);
        iTween.MoveTo(gameObject, parameters);
    }

    void MoveOut()
    {
        Hashtable parameters = new Hashtable();
        parameters.Add("y", -Screen.height * 0.5f);
        parameters.Add("easeType", iTween.EaseType.easeInOutBack);
        parameters.Add("oncomplete", "PanelDestroy");
        parameters.Add("oncompletetarget", gameObject);
        iTween.MoveTo(gameObject, parameters);
    }
    
    void PanelDestroy()
    {
        Destroy(gameObject);
    }
}