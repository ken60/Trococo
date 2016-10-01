using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Panel_HowToPlay : MonoBehaviour
{
    [SerializeField]
    private Sprite[] m_Sprite;
    [SerializeField]
    private Button[] m_Button;
    [SerializeField]
    private Image m_ExplanatoryImage;

    private int m_PageNmmber = 1;

    void Start()
    {
        transform.localScale = Vector3.zero;

        m_ExplanatoryImage.sprite = m_Sprite[m_PageNmmber - 1];
        m_Button[0].gameObject.SetActive(false);
        m_Button[1].gameObject.SetActive(true);
    }

    void MyUpdate()
    {
        //1ページ目の時は戻るボタンを隠す
        if (m_PageNmmber == 1)
            m_Button[0].gameObject.SetActive(false);
        else
            m_Button[0].gameObject.SetActive(true);

        //最後のページの時はボタンテキストを「もどる」に
        if (m_PageNmmber == m_Sprite.Length)
            m_Button[1].gameObject.SetActive(false);
        else
            m_Button[1].gameObject.SetActive(true);

        m_ExplanatoryImage.sprite = m_Sprite[m_PageNmmber - 1];
    }

    //「もどる」ボタンをおした時
    public void Button_Prev()
    {
        //ページ数が1ページ未満の時ページ送り
        if (1 < m_PageNmmber)
            m_PageNmmber--;

        MyUpdate();
    }

    //「つぎへ」ボタンをおした時
    public void Button_Next()
    {
        //ページ数が最大未満の時ページ送り
        if (m_PageNmmber < m_Sprite.Length)
            m_PageNmmber++;

        MyUpdate();
    }

    public int pageNumber
    {
        get { return m_PageNmmber; }
        set { m_PageNmmber = value; }
    }
}