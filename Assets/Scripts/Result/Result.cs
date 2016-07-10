using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    RectTransform m_RectTransform;

    void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();
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
        GameData.Instance.InitGame();
        SceneManager.LoadScene("Title");
    }

    void Restart()
    {
        GameData.Instance.InitGame();
        SceneManager.LoadScene("Game");
    }

}