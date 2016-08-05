using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Panel_Title : MonoBehaviour
{
    [SerializeField]
    private Panel_HowToPlay m_Panel_HowToPlay;
    [SerializeField]
    private Panel_CharSelect m_Panel_CharSelect;
    [SerializeField]
    private Text m_GoldCoinText;
    [SerializeField]
    private Text m_CopperCoinText;

    private int m_ClickCnt = 0;

    void Start()
    {

    }

    void Update()
    {
        m_GoldCoinText.text = GameManager.Instance.totalGoldCoinNum.ToString();
        m_CopperCoinText.text = GameManager.Instance.totalCopperCoinNum.ToString();
    }

    //********Button********
    public void Button_GameStart()
    {
        m_ClickCnt++;
        if (m_ClickCnt != 1) return;
        MoveOut_Start();
    }

    public void Button_Select()
    {
        m_ClickCnt++;
        if (m_ClickCnt != 1) return;
        MoveOut();
        m_Panel_CharSelect.MoveIn();
    }

    public void Button_HowToPlay()
    {
        m_ClickCnt++;
        if (m_ClickCnt != 1) return;
        MoveOut();
        m_Panel_HowToPlay.MoveIn();
    }

    public void Button_Ranking()
    {
        if (m_ClickCnt != 1) return;
        if (Application.platform == RuntimePlatform.Android)
        {
            RankParkInterface.Instance().StartActivity();
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

    void MoveOut_Start()
    {
        Hashtable parameters = new Hashtable();
        parameters.Add("y", -Screen.height * 0.5f);
        parameters.Add("time", 0.4f);
        parameters.Add("easeType", iTween.EaseType.easeInOutSine);
        parameters.Add("oncomplete", "LoadGame");
        parameters.Add("oncompletetarget", gameObject);
        iTween.MoveTo(gameObject, parameters);
    }

    void MoveOut()
    {
        Hashtable parameters = new Hashtable();
        parameters.Add("y", -Screen.height * 0.5f);
        parameters.Add("time", 0.4f);
        parameters.Add("easeType", iTween.EaseType.easeInOutSine);
        parameters.Add("oncomplete", "PanelMove");
        parameters.Add("oncompletetarget", gameObject);
        iTween.MoveTo(gameObject, parameters);
    }

    void LoadGame()
    {
        GameScene.m_GameScene = GameScene.eGameScene.LoadGame;
        transform.localPosition = new Vector2(0.0f, 1920.0f);
        m_ClickCnt = 0;
    }

    void PanelMove()
    {
        m_ClickCnt = 0;
        transform.localPosition = new Vector2(0.0f, 1920.0f);
    }
}