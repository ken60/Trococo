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


    void Start()
    {

    }

    void Update()
    {
        m_GoldCoinText.text = GameManager.Instance.totalGoldCoinNum.ToString();
        m_CopperCoinText.text = GameManager.Instance.totalCopperCoinNum.ToString();
    }

    public void Button_GameStart()
    {
        MoveOut_Start();
    }

    public void Button_Select()
    {
        MoveOut();
        m_Panel_CharSelect.MoveIn();
    }

    public void Button_HowToPlay()
    {
        MoveOut();
        m_Panel_HowToPlay.MoveIn();
    }

    public void Button_Ranking()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            RankParkInterface.Instance().StartActivity();
        }
    }

    public void MoveIn()
    {
        Hashtable parameters = new Hashtable();
        parameters.Add("y", Screen.height * 0.5f);
        parameters.Add("time", 0.4f);
        parameters.Add("easeType", iTween.EaseType.easeInOutSine);
        iTween.MoveTo(gameObject, parameters);
    }

    public void MoveOut_Start()
    {
        Hashtable parameters = new Hashtable();
        parameters.Add("y", -Screen.height * 0.5f);
        parameters.Add("time", 0.4f);
        parameters.Add("easeType", iTween.EaseType.easeInOutSine);
        parameters.Add("oncomplete", "LoadGame");
        parameters.Add("oncompletetarget", gameObject);
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

    void LoadGame()
    {
        GameScene.m_GameScene = GameScene.eGameScene.LoadGame;
        transform.localPosition = new Vector2(0.0f, 1650.0f);
    }

    void PanelMove()
    {
        transform.localPosition = new Vector2(0.0f, 1650.0f);
    }
}