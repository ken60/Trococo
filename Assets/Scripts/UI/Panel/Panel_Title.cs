using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class Panel_Title : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Panel_HowToPlay;
    [SerializeField]
    private GameObject m_Panel_CharSelect;
    [SerializeField]
    private Text m_Text;

    private GameObject m_Canvas;

    void Start()
    {
        m_Canvas = GameObject.Find("Canvas");

        MoveIn();
    }

    void Update()
    {
        m_Text.text = "所持コイン " + GameManager.Instance.totalCoinNum + "枚";
    }

    public void Button_GameStart()
    {
        MoveOut_Start();
    }

    public void Button_Select()
    {
        MoveOut();
        GameObject panel = Instantiate(m_Panel_CharSelect, new Vector3(0.0f, 1900.0f, 0.0f), Quaternion.identity) as GameObject;
        panel.transform.SetParent(m_Canvas.transform, false);
    }

    public void Button_HowToPlay()
    {
        MoveOut();
        GameObject panel = Instantiate(m_Panel_HowToPlay, new Vector3(0.0f, 1900.0f, 0.0f), Quaternion.identity) as GameObject;
        panel.transform.SetParent(m_Canvas.transform, false);
    }

    public void Button_Ranking()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            RankParkInterface.Instance().StartActivity();
        }
    }

    void MoveIn()
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
        parameters.Add("oncomplete", "PanelDestroy");
        parameters.Add("oncompletetarget", gameObject);
        iTween.MoveTo(gameObject, parameters);
    }

    void LoadGame()
    {
        GameScene.m_GameScene = GameScene.eGameScene.LoadGame;
        Destroy(gameObject);
    }

    void PanelDestroy()
    {
        Destroy(gameObject);
    }
}