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

    private GameObject m_Canvas;

    void Start()
    {
        m_Canvas = GameObject.Find("Canvas");

        //セーブデータのロード
        GameManager.Instance.LoadGame();

        MoveIn();
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
        parameters.Add("easeType", iTween.EaseType.easeInOutBack);
        iTween.MoveTo(gameObject, parameters);
    }

    void MoveOut_Start()
    {
        Hashtable parameters = new Hashtable();
        parameters.Add("y", -Screen.height * 0.5f);
        parameters.Add("easeType", iTween.EaseType.easeInOutBack);
        parameters.Add("oncomplete", "LoadGame");
        parameters.Add("oncompletetarget", gameObject);
        iTween.MoveTo(gameObject, parameters);
    }

    void MoveOut()
    {
        Hashtable parameters = new Hashtable();
        parameters.Add("y", -Screen.height * 0.5f);
        parameters.Add("easeType", iTween.EaseType.easeInOutBack);
        parameters.Add("oncomplete", "PanelDestroy");
        parameters.Add("oncompletetarget", gameObject);
        iTween.MoveTo(gameObject, parameters);
    }

    void LoadGame()
    {
        SceneManager.LoadScene("Game");
        Destroy(gameObject);
    }

    void PanelDestroy()
    {
        Destroy(gameObject);
    }
}