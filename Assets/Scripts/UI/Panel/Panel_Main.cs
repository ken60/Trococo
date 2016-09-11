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
    private int m_OldShowingPanelNum;
    private int m_ClickCnt = 0;
    private bool m_isActive_itween = false; //itween動作中か

    void Start()
    {
        m_OldShowingPanelNum = m_ShowingPanelNum;

        //全てのメニューパネルを非アクティブ化
        foreach (GameObject obj in m_Panel_Menu)
            obj.SetActive(false);

        //ホームパネルをアクティブ化
        m_Panel_Menu[m_ShowingPanelNum].SetActive(true);

        //表示中のパネルのボタンを無効化
        m_Button_Menu[m_ShowingPanelNum].enabled = false;

        transform.localScale = Vector3.zero;
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

        //セッティングパネルを閉じた時にセーブ
        if (m_ShowingPanelNum == (int)PanelMenuNum.ePanel_Settings)
            if (num != m_ShowingPanelNum && GameManager.Instance.isChangingSettings())
            {
                GameManager.Instance.SaveGame();
                print("Save");
            }

        //表示していたパネルのボタンを有効化
        m_Button_Menu[m_ShowingPanelNum].enabled = true;
        //表示していたパネルを隠す
        PanelHide(m_Panel_Menu[m_ShowingPanelNum].gameObject);
        //押したボタンのパネルを表示
        m_Panel_Menu[num].SetActive(true);
        iTweenManager.Show_ScaleTo(m_Panel_Menu[num].gameObject, 0.2f);
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

        iTweenManager.MoveOut_MoveTo_Y(this.gameObject, 0.4f, -Screen.height * 0.5f, "LoadGame", this.gameObject);
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
            //ランキングを表示
        }
        else
        {
            Debug.Log("Show RankPark");
        }
    }

    //メニューパネルを非表示
    void PanelHide(GameObject panel)
    {
        m_isActive_itween = true;
        Hashtable hash = new Hashtable();
        hash.Add("x", 0.0f);
        hash.Add("y", 0.0f);
        hash.Add("time", 0.2f);
        hash.Add("easeType", iTween.EaseType.easeInOutSine);
        hash.Add("oncomplete", "EndAction");
        hash.Add("oncompletetarget", this.gameObject);
        iTween.ScaleTo(panel.gameObject, hash);
    }

    void EndAction()
    {
        m_isActive_itween = false;
        m_Panel_Menu[m_OldShowingPanelNum].SetActive(false);
        m_OldShowingPanelNum = m_ShowingPanelNum;
    }

    void LoadGame()
    {
        GameScene.m_GameScene = GameScene.eGameScene.LoadGame;
        Destroy(this.gameObject);
    }
}