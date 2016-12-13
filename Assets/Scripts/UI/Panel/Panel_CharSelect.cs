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
    private float m_TimeCount = 0f;

    void Start()
    {
        //スライダーの範囲を設定
        m_Slider.minValue = 0;
        m_Slider.maxValue = m_Sprite.Length - 1;

        int id = GameDataManager.Instance.playCharID;
        //スライダーを更新
        m_Slider.value = id;
        m_PageNmmber = id;
        //スプライトを変更
        m_ExplanatoryImage.sprite = m_Sprite[id];
    }

    void Update()
    {
        m_TimeCount += Time.deltaTime;
    }

    public void CharSelectUpdate()
    {
        //キャラが開放されている時
        if (GameDataManager.Instance.IsCharAvailable(m_PageNmmber))
        {
            //画像を元に戻す
            m_ExplanatoryImage.color = Color.white;
            //ボタンを有効化
            m_Button_Start.enabled = true;
            //テキストを変更
            m_StartButtonText.text = "ゲームスタート";
            m_StartButtonText.fontSize = 64;
        }
        else //キャラが開放されていない時
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
        GameDataManager.Instance.playCharID = m_PageNmmber;
    }

    //「もどる」ボタンをおした時
    public void Button_Prev()
    {
        if (m_TimeCount < 0.3f) return;
        m_TimeCount = 0f;

        //ページ数変更
        if (0 < m_PageNmmber)
            m_PageNmmber--;
        else
            m_PageNmmber = m_Sprite.Length - 1;

        CharSelectUpdate();
    }

    //「つぎへ」ボタンをおした時
    public void Button_Next()
    {
        if (m_TimeCount < 0.3f) return;
        m_TimeCount = 0f;

        //ページ数変更
        if (m_PageNmmber < m_Sprite.Length - 1)
            m_PageNmmber++;
        else
            m_PageNmmber = 0;

        CharSelectUpdate();
    }
}