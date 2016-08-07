using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Panel_HowToPlay : MonoBehaviour
{
    [SerializeField]
    private Sprite[] m_Sprite;
    [SerializeField]
    private Text[] m_ButtonText;
    [SerializeField]
    private Image m_ExplanatoryImage;
    [SerializeField]
    private Panel_Main m_Panel_Title;

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
        
    }

    //「つぎへ」ボタンをおした時
    public void Button_Next()
    {
        //ページ数が最大未満の時ページ送り
        if (m_PageNmmber < m_Sprite.Length)
            m_PageNmmber++;        
    }
}