using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
    enum eGameScene
    {
        LoadGame = 0,
        StartCount,
        Play,
        GameOver,
        End
    }

    [SerializeField]
    private GameObject m_Panel_Result;
    [SerializeField]
    private GameObject m_StartCount;
    [SerializeField]
    private Canvas m_Canvas;
    [SerializeField]
    private float m_GameOverWait;

    private Camera m_Camera;
    private eGameScene m_GameScene;
    private float m_TimeCount = 0.0f;


    void Start()
    {
        m_Camera = Camera.main;
    }

    void Update()
    {
        switch (m_GameScene)
        {
            case eGameScene.LoadGame:
                GameManager.Instance.LoadGame();
                GameObject obj = Instantiate(m_StartCount, new Vector3(0.0f, 0.0f, 12.0f), Quaternion.identity) as GameObject;
                obj.transform.SetParent(m_Camera.transform, false);
                m_GameScene++;
                break;

            //ゲーム開始時のカウントダウン
            case eGameScene.StartCount:
                m_TimeCount += Time.deltaTime;

                if (m_TimeCount >= 3.0f)
                {
                    m_TimeCount = 0.0f;
                    GameSceneManager.Instance.IsGamePlaying = true;
                    m_GameScene++;
                }
                break;
            //ゲームプレイ中
            case eGameScene.Play:
                if (GameSceneManager.Instance.IsGameOver)
                {
                    m_TimeCount += Time.deltaTime;
                    if (m_TimeCount >= m_GameOverWait)
                    {
                        m_TimeCount = 0.0f;
                        m_GameScene++;
                    }
                }
                break;
            //ゲームオーバー時一度だけ
            case eGameScene.GameOver:
                GameSceneManager.Instance.IsGamePlaying = false;
                GameObject panel = Instantiate(m_Panel_Result, new Vector3(0.0f, 1645.0f, 0.0f), Quaternion.identity) as GameObject;
                panel.transform.SetParent(m_Canvas.transform, false);

                //ハイスコアの時 & Androidのみスコアを送信
                if (GameManager.Instance.IsHighScore() && Application.platform == RuntimePlatform.Android)
                {
                    RankParkInterface.Instance().AddScore(GameManager.Instance.Score);
                }
                //セーブ
                GameManager.Instance.SaveGame();

                m_GameScene++;
                break;

            case eGameScene.End:
                break;
        }
    }
}