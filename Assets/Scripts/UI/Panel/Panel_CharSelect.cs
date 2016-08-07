﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Panel_CharSelect : MonoBehaviour
{
    [SerializeField]
    private Sprite[] m_Sprite;          //各キャラの画像
    [SerializeField]
    private Text[] m_ButtonText;        //ボタンテキスト
    [SerializeField, TextArea(3, 24)]
    private string[] m_DescriptionText; //キャラクターの説明文
    [SerializeField, TextArea(2, 24)]
    private string m_NotAvailableText;  //キャラ未開放時に追加するテキスト
    [SerializeField]
    private Text m_Description;         //キャラクターの説明Text
    [SerializeField]
    private Button m_ButtonDecision;    //決定ボタン
    [SerializeField]
    private Image m_ExplanatoryImage;   //キャラクターのImage
    [SerializeField]
    private Panel_Title m_Panel_Title;

    private int m_PageNmmber = 1;

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

        //キャラが開放されている時
        if (GameManager.Instance.isCharAvailable(m_PageNmmber - 1))
        {
            //画像を元に戻す
            m_ExplanatoryImage.color = Color.white;
            //決定ボタン表示
            m_ButtonDecision.gameObject.SetActive(true);
            //説明文を変更
            m_Description.text = m_DescriptionText[m_PageNmmber - 1];
        }
        //キャラが開放されていない時
        else
        {
            //画像を黒くする
            m_ExplanatoryImage.color = Color.black;
            //決定ボタンを非表示
            m_ButtonDecision.gameObject.SetActive(false);
            //説明文に未開放の旨を記載
            m_Description.text = m_NotAvailableText + "\n" + m_DescriptionText[m_PageNmmber - 1];
        }

        //スプライトと説明文を変更
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

    //決定ボタンをおした時
    public void Button_Decision()
    {
        //選択中のキャラをプレイキャラに設定
        GameManager.Instance.playCharID = m_PageNmmber - 1;

        MoveOut();
        m_Panel_Title.MoveIn();
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