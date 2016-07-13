using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class Panel_Title : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Panel_HowToPlay;

    private RectTransform m_RectTransform;

    void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();

        //セーブデータのロード
        GameManager.Instance.LoadGame();

        MoveIn();
    }

    public void Button_GameStart()
    {
        MoveOut_Start();
    }

    public void Button_HowToPlay()
    {

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
        parameters.Add("easeType", iTween.EaseType.easeOutBounce);
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

    void LoadGame()
    {
        SceneManager.LoadScene("Game");
        Destroy(gameObject);
    }
}