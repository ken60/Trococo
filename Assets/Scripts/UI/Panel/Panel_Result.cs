using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Panel_Result : MonoBehaviour
{
    [SerializeField]
    private Text m_ScoreNum;
    [SerializeField]
    private Text m_CoinNum;

    private RectTransform m_RectTransform;


    void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();
        m_ScoreNum.text = GameManager.Instance.score + " m";
        m_CoinNum.text = GameManager.Instance.coin + " 枚";
        MoveIn();
    }

    void Update()
    {

    }

    //iTween
    void MoveIn()
    {
        Hashtable parameters = new Hashtable();
        parameters.Add("y", Screen.height * 0.5f);
        parameters.Add("time", 0.4f);
        parameters.Add("easeType", iTween.EaseType.easeInOutSine);
        iTween.MoveTo(gameObject, parameters);
    }

    public void Button_Title()
    {
        Hashtable parameters = new Hashtable();
        parameters.Add("y", -Screen.height + m_RectTransform.sizeDelta.y);
        parameters.Add("time", 0.4f);
        parameters.Add("easeType", iTween.EaseType.easeInOutSine);
        parameters.Add("oncomplete", "BackToTitle");
        parameters.Add("oncompletetarget", gameObject);
        iTween.MoveTo(gameObject, parameters);
    }

    public void Button_Restart()
    {
        Hashtable parameters = new Hashtable();
        parameters.Add("y", -Screen.height + m_RectTransform.sizeDelta.y);
        parameters.Add("time", 0.4f);
        parameters.Add("easeType", iTween.EaseType.easeInOutSine);
        parameters.Add("oncomplete", "Restart");
        parameters.Add("oncompletetarget", gameObject);
        iTween.MoveTo(gameObject, parameters);
    }

    //ボタンクリック後のアニメーション完了時に呼ばれる
    void BackToTitle()
    {
        GameManager.Instance.InitGame();
        GameScene.m_GameScene = GameScene.eGameScene.LoadTitle;
    }

    void Restart()
    {
        GameManager.Instance.InitGame();
        GameScene.m_GameScene = GameScene.eGameScene.LoadGame;
    }

}