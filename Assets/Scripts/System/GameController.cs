using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    enum GameScene
    {
        Title = 0,
        LoadGame,
        StartCount,
        Play,
        GameOver,
        End
    }

    [SerializeField]
    private GameObject m_ResultPanel;
    [SerializeField]
    private GameObject m_StartCount;
    [SerializeField]
    private Canvas m_Canvas;
    [SerializeField]
    private float m_GameOverWait;

    private GameScene m_GameScene;
    private float m_TimeCount = 0.0f;


    void Start()
    {

    }

    void Update()
    {
        switch (m_GameScene)
        {
            case GameScene.Title:
                m_GameScene++;
                break;

            case GameScene.LoadGame:
                //セーブデータのロード
                //GameManager.Instance.LoadGame();
                m_GameScene++;
                break;

            //ゲーム開始時のカウントダウン
            case GameScene.StartCount:
                m_TimeCount += Time.deltaTime;

                if (m_TimeCount >= 4.0f)
                {
                    m_TimeCount = 0.0f;
                    GameManager.Instance.IsGamePlay = true;
                    m_GameScene++;
                }
                break;
            //ゲームプレイ中
            case GameScene.Play:
                if (GameManager.Instance.IsGameOver)
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
            case GameScene.GameOver:
                GameManager.Instance.IsGamePlay = false;
                GameObject panel = Instantiate(m_ResultPanel, new Vector3(0.0f, 1645.0f, 0.0f), Quaternion.identity) as GameObject;
                panel.transform.SetParent(m_Canvas.transform, false);

                //ハイスコアの時 & Androidのみスコアを送信
                if (GameManager.Instance.IsHighScore() && Application.platform == RuntimePlatform.Android)
                {
                    RankParkInterface.Instance().AddScore(GameManager.Instance.Score);
                }

                //セーブ
                //GameManager.Instance.SaveGame();

                m_GameScene++;
                break;

            case GameScene.End:
                break;
        }
    }
}