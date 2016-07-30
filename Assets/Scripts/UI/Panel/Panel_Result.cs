using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Panel_Result : MonoBehaviour
{
    [SerializeField]
    private SceneChangeFade m_Fade;
    [SerializeField]
    private Text m_ScoreNum;
    [SerializeField]
    private Text m_CoinNum;

    private RectTransform m_RectTransform;


    void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        m_ScoreNum.text = GameManager.Instance.score + " m";
        m_CoinNum.text = GameManager.Instance.coin + " 枚";
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

        DestroySquidInk();
        GameScene.m_GameScene = GameScene.eGameScene.LoadTitle;
        m_Fade.FadeIn();
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

        DestroySquidInk();
        GameScene.m_GameScene = GameScene.eGameScene.LoadGame;
        m_Fade.FadeIn();
    }

    //iTween
    public void MoveIn()
    {
        Hashtable parameters = new Hashtable();
        parameters.Add("y", Screen.height * 0.5f);
        parameters.Add("time", 0.4f);
        parameters.Add("easeType", iTween.EaseType.easeInOutSine);
        iTween.MoveTo(gameObject, parameters);
    }

    //ボタンクリック後のアニメーション完了時に呼ばれる
    void BackToTitle()
    {
        transform.localPosition = new Vector2(0.0f, 1645.0f);
    }

    void Restart()
    {
        transform.localPosition = new Vector2(0.0f, 1645.0f);
    }

    //画面に出たイカスミを削除
    void DestroySquidInk()
    {
        GameObject[] tagobjs = GameObject.FindGameObjectsWithTag("Squid_ink");
        foreach (GameObject obj in tagobjs)
        {
            Destroy(obj);
        }
    }
}