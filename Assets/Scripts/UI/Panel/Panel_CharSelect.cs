using UnityEngine;
using UnityEngine.UI;

public class Panel_CharSelect : MonoBehaviour
{
    [SerializeField]
    private Sprite[] m_Sprite;          //各キャラの画像(全キャラの数)
    [SerializeField]
    private Button[] m_Botton_Arrow;      //左右矢印ボタン
    [SerializeField]
    private Text m_StartButtonText;     //ボタンテキスト
    [SerializeField]
    private Image m_ExplanatoryImage;   //キャラクターのImage
    [SerializeField]
    private Button m_Button_Start;      //スタートボタン
    [SerializeField]
    private Slider m_Slider;

    private int m_PageNmmber = 0;

    void Start()
    {
        //スライダーの範囲を設定
        m_Slider.minValue = 0;
        m_Slider.maxValue = m_Sprite.Length - 1;

        int id = GameManager.Instance.playCharID;
        //スライダーを更新
        m_Slider.value = id;
        m_PageNmmber = id;
        //スプライトを変更
        m_ExplanatoryImage.sprite = m_Sprite[id];

        Button_Arrow();
    }

    void Button_Arrow()
    {
        //1ページ目は◀ボタンを隠す
        if (m_PageNmmber == 0)
            m_Botton_Arrow[0].gameObject.SetActive(false);
        else
            m_Botton_Arrow[0].gameObject.SetActive(true);

        //最終ページは▶ボタンを隠す
        if (m_PageNmmber == m_Sprite.Length - 1)
            m_Botton_Arrow[1].gameObject.SetActive(false);
        else
            m_Botton_Arrow[1].gameObject.SetActive(true);
    }

    public void CharSelectUpdate()
    {
        Button_Arrow();

        //キャラが開放されている時
        if (GameManager.Instance.IsCharAvailable(m_PageNmmber))
        {
            //画像を元に戻す
            m_ExplanatoryImage.color = Color.white;
            //ボタンを有効化
            m_Button_Start.enabled = true;
            //テキストを変更
            m_StartButtonText.text = "ゲームスタート";
            m_StartButtonText.fontSize = 64;
        }
        //キャラが開放されていない時
        else
        {
            //画像を黒くする
            m_ExplanatoryImage.color = Color.black;
            //ボタンを無効化
            m_Button_Start.enabled = false;
            //テキストを変更
            m_StartButtonText.text = "?";
            m_StartButtonText.fontSize = 100;
        }

        //スプライトを変更
        m_ExplanatoryImage.sprite = m_Sprite[m_PageNmmber];
        //スライダーを更新
        m_Slider.value = m_PageNmmber;
        //キャラIDを変更
        GameManager.Instance.playCharID = m_PageNmmber;
    }

    //「もどる」ボタンをおした時
    public void Button_Prev()
    {
        //ページ数が1ページ未満の時ページ送り
        if (0 < m_PageNmmber)
            m_PageNmmber--;

        CharSelectUpdate();
    }

    //「つぎへ」ボタンをおした時
    public void Button_Next()
    {
        //ページ数が最大未満の時ページ送り
        if (m_PageNmmber < m_Sprite.Length - 1)
            m_PageNmmber++;

        CharSelectUpdate();
    }
}