using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    enum GameScene
    {
        StartCount = 0,
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
            //ゲーム開始時のカウントダウン
            case GameScene.StartCount:
                m_TimeCount += Time.deltaTime;

                if (m_TimeCount >= 3.0f)
                {
                    m_TimeCount = 0.0f;
                    GameData.Instance.IsGamePlay = true;
                    m_GameScene++;
                }
                break;
            //ゲームプレイ中
            case GameScene.Play:
                if (GameData.Instance.IsGameOver)
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
                GameData.Instance.IsGamePlay = false;
                GameObject panel = Instantiate(m_ResultPanel, new Vector3(0.0f, 1645.0f, 0.0f), Quaternion.identity) as GameObject;
                panel.transform.SetParent(m_Canvas.transform, false);

                //Androidのみスコアを送信
                if (Application.platform == RuntimePlatform.Android)
                {
                    RankParkInterface.Instance().AddScore(GameData.Instance.Score);
                }
                m_GameScene++;
                break;

            case GameScene.End:
                break;
        }
    }
}