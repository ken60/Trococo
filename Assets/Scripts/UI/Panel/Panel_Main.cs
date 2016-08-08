using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Panel_Main : MonoBehaviour
{
    enum PanelMenuNum
    {
        ePanel_Settings = 0,
        ePanel_Home,
        ePanel_Gashapon,
        ePanel_HowToPlay,
    }

    [SerializeField]
    private GameObject[] m_Panel_Menu = null;
    [SerializeField]
    private Button[] m_Button_Menu = null;
    [SerializeField]
    private Panel_CharSelect m_CharSelect = null;
    [SerializeField]
    private Text m_GoldCoinText = null;
    [SerializeField]
    private Text m_CopperCoinText = null;
    [SerializeField]
    private float m_LerpLate = 1.0f;

    private int m_ShowingPanelNum = (int)PanelMenuNum.ePanel_Home;
    private int m_ClickCnt = 0;
    private bool m_isActive_itween = false; //itween動作中か

    void Start()
    {
        //表示中のパネルのボタンを無効化
        m_Button_Menu[m_ShowingPanelNum].enabled = false;

        transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
    }

    void Update()
    {
        //累計コイン数を表示
        m_GoldCoinText.text = GameManager.Instance.totalGoldCoinNum.ToString();
        
    }

    //メニューボタンにアタッチ
    public void MenuButton(int num)
    {
        if (m_isActive_itween) return;

        //表示中のパネルのボタンを無効化
        m_Button_Menu[num].enabled = false;

        //ボタンの同時押し対策
        m_ClickCnt++;
        if (m_ClickCnt != 1) return;
        m_ClickCnt = 0;

        //表示していたパネルのボタンを有効化
        m_Button_Menu[m_ShowingPanelNum].enabled = true;
        //押したボタンのパネルを表示
        PanelShow(m_Panel_Menu[num].gameObject);
        //前回表示していたパネルを隠す
        PanelHide(m_Panel_Menu[m_ShowingPanelNum].gameObject);
        //表示中のパネルを変更
        m_ShowingPanelNum = num;
    }

    //ゲームスタートボタン
    public void Button_GameStart()
    {
        //ボタンの同時押し対策
        m_ClickCnt++;
        if (m_ClickCnt != 1) return;
        m_ClickCnt = 0;

        MoveOut();
    }

    //ランキング
    public void Button_Ranking()
    {
        //ボタンの同時押し対策
        m_ClickCnt++;
        if (m_ClickCnt != 1) return;
        m_ClickCnt = 0;

        if (Application.platform == RuntimePlatform.Android)
        {
            //RankParkを表示
            RankParkInterface.Instance().StartActivity();
        }
        else
        {
            Debug.Log("Show RankPark");
        }
    }

    //********iTween********
    public void Show()
    {
        Hashtable hash = new Hashtable();
        hash.Add("x", 1.0f);
        hash.Add("y", 1.0f);
        hash.Add("time", 0.4f);
        hash.Add("easeType", iTween.EaseType.easeInOutSine);
        iTween.ScaleTo(this.gameObject, hash);
    }

    public void MoveOut()
    {
        Hashtable hash = new Hashtable();
        hash.Add("y", -Screen.height * 0.5f);
        hash.Add("time", 0.4f);
        hash.Add("easeType", iTween.EaseType.easeInOutSine);
        hash.Add("oncomplete", "LoadGame");
        hash.Add("oncompletetarget", this.gameObject);
        iTween.MoveTo(this.gameObject, hash);
    }

    //メニューパネルを表示
    void PanelShow(GameObject panel)
    {
        Hashtable hash = new Hashtable();
        hash.Add("x", 1.0f);
        hash.Add("y", 1.0f);
        hash.Add("time", 0.2f);
        hash.Add("easeType", iTween.EaseType.easeInOutSine);
        hash.Add("onstart", "StartAction");
        hash.Add("oncompletetarget", this.gameObject);
        iTween.ScaleTo(panel.gameObject, hash);
    }

    //メニューパネルを非表示
    void PanelHide(GameObject panel)
    {
        Hashtable hash = new Hashtable();
        hash.Add("x", 0.0f);
        hash.Add("y", 0.0f);
        hash.Add("time", 0.2f);
        hash.Add("easeType", iTween.EaseType.easeInOutSine);
        hash.Add("oncomplete", "EndAction");
        hash.Add("oncompletetarget", this.gameObject);
        iTween.ScaleTo(panel.gameObject, hash);
    }
    
    void StartAction()
    {
        m_isActive_itween = true;
    }

    void EndAction()
    {
        m_isActive_itween = false;
    }

    void LoadGame()
    {
        GameScene.m_GameScene = GameScene.eGameScene.LoadGame;
        Destroy(this.gameObject);
    }


}