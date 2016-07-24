using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    [SerializeField]
    private Text m_ScoreNum;
    [SerializeField]
    private Text m_CoinNum;
    [SerializeField]
    private GameObject m_Panel_Title;

    private GameObject m_Canvas;

    private RectTransform m_RectTransform;


    void Start()
    {
        m_Canvas = GameObject.Find("Canvas");
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
        parameters.Add("easeType", iTween.EaseType.easeInOutBack);
        iTween.MoveTo(gameObject, parameters);
    }

    public void Button_Title()
    {
        Hashtable parameters = new Hashtable();
        parameters.Add("y", -Screen.height + m_RectTransform.sizeDelta.y);
        parameters.Add("easeType", iTween.EaseType.easeInOutBack);
        parameters.Add("oncomplete", "BackToTitle");
        parameters.Add("oncompletetarget", gameObject);
        iTween.MoveTo(gameObject, parameters);

        //タイトルパネルの表示
        GameObject panelTitle = Instantiate(m_Panel_Title, new Vector3(0.0f, 1645.0f, 0.0f), Quaternion.identity) as GameObject;
        panelTitle.transform.SetParent(m_Canvas.transform, false);
    }

    public void Button_Restart()
    {
        Hashtable parameters = new Hashtable();
        parameters.Add("y", -Screen.height + m_RectTransform.sizeDelta.y);
        parameters.Add("easeType", iTween.EaseType.easeInOutBack);
        parameters.Add("oncomplete", "Restart");
        parameters.Add("oncompletetarget", gameObject);
        iTween.MoveTo(gameObject, parameters);
    }

    //ボタンクリック後のアニメーション完了時に呼ばれる
    void BackToTitle()
    {
        GameManager.Instance.InitGame();
        GameSceneManager.scene = GameSceneManager.eGameScene.LoadTitle;
    }

    void Restart()
    {
        GameManager.Instance.InitGame();
        GameSceneManager.scene = GameSceneManager.eGameScene.LoadGame;
    }

}