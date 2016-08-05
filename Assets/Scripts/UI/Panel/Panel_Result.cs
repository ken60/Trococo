using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Advertisements;

public class Panel_Result : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_ResultBar;
    [SerializeField]
    private GameObject[] m_Button;
    [SerializeField]
    private Button m_Button_Ads;
    [SerializeField]
    private Text m_ScoreNum;
    [SerializeField]
    private Text m_GoldCoinNum;
    [SerializeField]
    private Text m_CopperCoinNum;
    [SerializeField]
    private Text m_AdsText;
    [SerializeField]
    private int m_ShowAdRate = 4;

    private RectTransform m_RectTransform;
    private int m_ClickCnt = 0;


    void Start()
    {
        m_ClickCnt = 0;
        m_RectTransform = GetComponent<RectTransform>();

        m_AdsText.text = "動画を見てコインをゲット!";

        int random = Random.Range(0, m_ShowAdRate);
        int i;
        //広告ありの場合
        if (random == 0)
        {
            //スコアバーなどの移動
            for (i = 0; i < m_ResultBar.Length; i++)
            {
                MoveIn(m_ResultBar[i].gameObject, i * 0.2f);
            }
            //ボタンの移動
            for (int j = 0; j < m_Button.Length; j++)
                Button_MoveIn(m_Button[j].gameObject, (j + m_ResultBar.Length) * 0.2f);
        }
        //広告無しの場合
        else
        {
            //スコアバーなどの移動
            for (i = 0; i < m_ResultBar.Length - 1; i++)
            {
                MoveIn(m_ResultBar[i].gameObject, i * 0.2f);

            }
            //ボタンの移動
            for (int j = 0; j < m_Button.Length; j++)
                Button_MoveIn(m_Button[j].gameObject, (j + m_ResultBar.Length - 1) * 0.2f);

        }

        //Textに結果を表示
        m_ScoreNum.text = GameManager.Instance.score + " m";
        m_GoldCoinNum.text = GameManager.Instance.goldCoin + " 枚";
        m_CopperCoinNum.text = GameManager.Instance.copperCoin + " 枚";
    }

    //Unity Ads
    public void ShowRewardedAD()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:

                m_Button_Ads.enabled = false;

                if (Random.Range(0, 3) == 0)
                {
                    m_AdsText.text = "ゴールドコイン +10枚！";
                    GameManager.Instance.goldCoin += 5;
                }
                else
                {
                    m_AdsText.text = "銅コイン +10枚！";
                    GameManager.Instance.copperCoin += 10;
                }

                GameManager.Instance.SaveGame();
                break;

            case ShowResult.Skipped:
                m_Button_Ads.enabled = false;
                Debug.Log("動画をスキップ");
                break;

            case ShowResult.Failed:
                m_Button_Ads.enabled = false;
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }

    //********iTween********
    void MoveIn(GameObject gameObj, float delay)
    {
        Hashtable parameters = new Hashtable();
        parameters.Add("x", Screen.width * 0.5f);
        parameters.Add("time", 0.4f);
        parameters.Add("delay", delay);
        parameters.Add("easeType", iTween.EaseType.easeInOutSine);

        iTween.MoveTo(gameObj, parameters);
    }

    void Button_MoveIn(GameObject gameObj, float delay)
    {
        Hashtable parameters = new Hashtable();
        parameters.Add("y", Screen.height * 0.15f);
        parameters.Add("time", 0.4f);
        parameters.Add("delay", delay);
        parameters.Add("easeType", iTween.EaseType.easeInOutSine);
        iTween.MoveTo(gameObj, parameters);
    }

    void MoveOut()
    {
        Hashtable parameters = new Hashtable();
        parameters.Add("y", -Screen.height + m_RectTransform.sizeDelta.y);
        parameters.Add("time", 0.4f);
        parameters.Add("easeType", iTween.EaseType.easeInOutSine);
        parameters.Add("oncomplete", "Destroy");
        parameters.Add("oncompletetarget", gameObject);
        iTween.MoveTo(gameObject, parameters);
    }

    //********Button********
    public void Button_Title()
    {
        m_ClickCnt++;
        if (m_ClickCnt != 1) return;
        MoveOut();
        DestroySquidInk();
        GameScene.m_GameScene = GameScene.eGameScene.LoadTitle;

    }

    public void Button_Restart()
    {
        m_ClickCnt++;
        if (m_ClickCnt != 1) return;
        MoveOut();
        DestroySquidInk();
        GameScene.m_GameScene = GameScene.eGameScene.LoadGame;
    }

    public void ViewAd()
    {
        ShowRewardedAD();
    }

    //アニメーション完了時に呼ばれる
    void Destroy()
    {
        Destroy(gameObject);
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