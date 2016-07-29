using UnityEngine;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
    public enum eGameScene
    {
        LoadTitle = 0,
        Title,
        LoadGame,
        StartCount,
        Play,
        GameOver,
        End
    }

    [HideInInspector]
    static public eGameScene m_GameScene;

    [SerializeField]
    private Text[] m_UI_Text;
    [SerializeField]
    private GameObject m_Panel_Title;
    [SerializeField]
    private GameObject m_Panel_Result;
    [SerializeField]
    private GameObject m_StartCount;
    [SerializeField]
    private GameObject m_Player;
    [SerializeField]
    private StageGenerator m_StageGenerator;
    [SerializeField]
    private float m_GameOverWait;


    private GameObject m_Canvas;
    private Camera m_Camera;
    private float m_TimeCount = 0.0f;
    private bool m_FromTitle = false;


    void Start()
    {
        m_FromTitle = false;
        m_Canvas = GameObject.Find("Canvas");
        m_Camera = Camera.main;
    }

    void Update()
    {
        switch (m_GameScene)
        {
            case eGameScene.LoadTitle:
                //セーブデータロード
                GameManager.Instance.LoadGame();
                //ステージの初期化
                m_StageGenerator.InitStage();
                //プレイヤーの初期化
                m_Player.GetComponent<Player>().InitPlayer();

                //ゲームプレイ中のUIを非表示
                for (int i = 0; i < m_UI_Text.Length; i++)
                {
                    m_UI_Text[i].enabled = false;
                }

                //タイトルパネルを表示
                GameObject panelTitle = Instantiate(m_Panel_Title, new Vector3(0.0f, 1645.0f, 0.0f), Quaternion.identity) as GameObject;
                panelTitle.transform.SetParent(m_Canvas.transform, false);

                m_FromTitle = true;

                m_GameScene = eGameScene.Title;
                break;

            case eGameScene.Title:
                //Show Title

                break;

            case eGameScene.LoadGame:
                GameSceneManager.Instance.isGameOver = false;

                //前のシーンがタイトル以外の時
                if (!m_FromTitle)
                {
                    //セーブデータロード
                    GameManager.Instance.LoadGame();
                    //ステージの初期化
                    m_StageGenerator.InitStage();
                    //プレイヤーの初期化
                    m_Player.GetComponent<Player>().InitPlayer();
                }

                //ゲームプレイ中のUIを表示
                for (int i = 0; i < m_UI_Text.Length; i++)
                {
                    m_UI_Text[i].enabled = true;
                }

                GameObject obj = Instantiate(m_StartCount, new Vector3(0.0f, 0.0f, 12.0f), Quaternion.identity) as GameObject;
                obj.transform.SetParent(m_Camera.transform, false);

                m_FromTitle = false;

                m_GameScene = eGameScene.StartCount;
                break;

            //ゲーム開始時のカウントダウン
            case eGameScene.StartCount:
                m_TimeCount += Time.deltaTime;

                if (m_TimeCount >= 3.0f)
                {
                    m_TimeCount = 0.0f;
                    GameSceneManager.Instance.isGamePlaying = true;
                    m_GameScene = eGameScene.Play;
                }
                break;
            //ゲームプレイ中
            case eGameScene.Play:
                if (GameSceneManager.Instance.isGameOver)
                {
                    m_TimeCount += Time.deltaTime;
                    if (m_TimeCount >= m_GameOverWait)
                    {
                        m_TimeCount = 0.0f;
                        m_GameScene = eGameScene.GameOver;
                    }
                }
                break;
            //ゲームオーバー時一度だけ
            case eGameScene.GameOver:
                GameSceneManager.Instance.isGamePlaying = false;
                GameObject panelRes = Instantiate(m_Panel_Result, new Vector3(0.0f, 1645.0f, 0.0f), Quaternion.identity) as GameObject;
                panelRes.transform.SetParent(m_Canvas.transform, false);

                //ハイスコアの時 & Androidのみスコアを送信
                if (GameManager.Instance.isHighScore() && Application.platform == RuntimePlatform.Android)
                {
                    RankParkInterface.Instance().AddScore(GameManager.Instance.score);
                    //print("Send Score");
                }
                //セーブ
                GameManager.Instance.SaveGame();

                m_GameScene = eGameScene.End;
                break;

            case eGameScene.End:
                break;
        }
    }
}