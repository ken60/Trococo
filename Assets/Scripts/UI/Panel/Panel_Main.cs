using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Panel_Main : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Panel_Menu;
    [SerializeField]
    private Panel_CharSelect m_CharSelect;
    [SerializeField]
    private Text m_GoldCoinText;
    [SerializeField]
    private Text m_CopperCoinText;
    [SerializeField]
    private float m_LerpLate = 1.0f;

    private int m_ClickCnt = 0;
    //**** m_ViewingPanelNum ****//
    //  Panel_Settings = 0
    //  Panel_Home = 1
    //  Panel_Gashapon = 2
    //  Panel_HowToPlay = 3
    //**************************//

    private float[] m_MenuPosition = new float[] { 1080.0f, 0.0f, -1080.0f, -2160.0f };

    void Start()
    {

    }

    void Update()
    {
        m_GoldCoinText.text = GameManager.Instance.totalGoldCoinNum.ToString();
    }

    //********Button********

    public void MenuButton(int num)
    {
        m_ClickCnt++;
        if (m_ClickCnt != 1) return;
        m_ClickCnt = 0;

        Slide(m_MenuPosition[num]);
    }

    //ゲームスタートボタン
    public void Button_GameStart()
    {
        m_ClickCnt++;
        if (m_ClickCnt != 1) return;
        m_ClickCnt = 0;
        MoveOut();
    }

    //ランキング
    public void Button_Ranking()
    {
        m_ClickCnt++;
        if (m_ClickCnt != 1) return;
        m_ClickCnt = 0;
        if (Application.platform == RuntimePlatform.Android)
        {
            RankParkInterface.Instance().StartActivity();
        }
        else
        {
            Debug.Log("Show RankPark");
        }
    }

    //********iTween********
    public void MoveIn()
    {
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
        parameters.Add("oncomplete", "LoadGame");
        parameters.Add("oncompletetarget", gameObject);
        iTween.MoveTo(gameObject, parameters);
    }

    public void Slide(float x)
    {
        Hashtable parameters = new Hashtable();
        parameters.Add("x", x - (-Screen.width * 0.5f));
        parameters.Add("time", 0.2f);
        parameters.Add("easeType", iTween.EaseType.easeInOutSine);
        iTween.MoveTo(m_Panel_Menu.gameObject, parameters);
    }

    void LoadGame()
    {
        GameScene.m_GameScene = GameScene.eGameScene.LoadGame;
        Destroy(gameObject);
    }


}