using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Panel_CharSelect : MonoBehaviour
{
    [SerializeField]
    private Sprite[] m_Sprite;
    [SerializeField]
    private Text[] m_ButtonText;
    [SerializeField]
    private Image m_ExplanatoryImage;
    [SerializeField]
    private Panel_Title m_Panel_Title;
    Button m_button;

    private int m_PageNmmber = 1;

    void Start()
    {

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
        if (m_PageNmmber == m_Sprite.Length)
        {
            m_ButtonText[1].text = "もどる";
        }
        else
        {
            m_ButtonText[1].text = "つぎへ";
        }

        m_ExplanatoryImage.sprite = m_Sprite[m_PageNmmber - 1];
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
            m_Panel_Title.MoveIn();
        }
    }

    //「つぎへ」ボタンをおした時
    public void Button_Next()
    {
        //ページ数が最大未満の時ページ送り
        if (m_PageNmmber < m_Sprite.Length)
            m_PageNmmber++;

        //最終ページの時はパネルを隠す
        if (m_ButtonText[1].text == "もどる")
        {
            MoveOut();
            m_Panel_Title.MoveIn();
        }
    }

    //iTween
    public void MoveIn()
    {
        m_PageNmmber = 1;
        Hashtable parameters = new Hashtable();
        parameters.Add("y", Screen.height * 0.5f);
        parameters.Add("time", 0.4f);
        parameters.Add("easeType", iTween.EaseType.easeInOutSine);
        iTween.MoveTo(gameObject, parameters);
    }

    public void MoveOut()
    {
        Hashtable parameters = new Hashtable();
        parameters.Add("y", -Screen.height * 0.5f);
        parameters.Add("time", 0.4f);
        parameters.Add("easeType", iTween.EaseType.easeInOutSine);
        parameters.Add("oncomplete", "PanelMove");
        parameters.Add("oncompletetarget", gameObject);
        iTween.MoveTo(gameObject, parameters);
    }

    void PanelMove()
    {
        transform.localPosition = new Vector2(0.0f, 1920.0f);
    }
}