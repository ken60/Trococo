using UnityEngine;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
    [SerializeField]
    private GameObject m_StageGen;
    [SerializeField]
    private GameObject m_Panel_Title;
    [SerializeField]
    private GameObject m_Panel_Result;
    [SerializeField]
    private GameObject m_StartCount;
    [SerializeField]
    private float m_GameOverWait;
    [SerializeField]
    private Text[] m_Text_UI;
    [SerializeField]
    private Player m_PlayerSrc;
    [SerializeField]
    private StageGenerator m_StageSrc;

    private GameObject m_Canvas;
    private GameObject stageGen;
    private Camera m_Camera;
    private float m_TimeCount = 0.0f;


    void Start()
    {
        m_Canvas = GameObject.Find("Canvas");
        m_Camera = Camera.main;

        //タイトルパネルの表示
        GameObject panelTitle = Instantiate(m_Panel_Title, new Vector3(0.0f, 1645.0f, 0.0f), Quaternion.identity) as GameObject;
        panelTitle.transform.SetParent(m_Canvas.transform, false);

        stageGen = Instantiate(m_StageGen, Vector3.zero, Quaternion.identity) as GameObject;

    }

    void Update()
    {
        switch (GameSceneManager.scene)
        {
            case GameSceneManager.eGameScene.LoadTitle:
                //セーブデータのロード
                GameManager.Instance.LoadGame();

                //キョリ、コインテキストを非表示
                foreach (Text tex in m_Text_UI)
                {
                    tex.gameObject.SetActive(false);
                }


                GameSceneManager.scene = GameSceneManager.eGameScene.Title;

                break;

            case GameSceneManager.eGameScene.Title:
                //タイトル表示中

                break;

            case GameSceneManager.eGameScene.LoadGame:
                //セーブデータのロード
                GameManager.Instance.LoadGame();
                GameSceneManager.isGameOver = false;
                //プレイヤーの初期化
                m_PlayerSrc.InitPlayer();
                //m_StageSrc.InitStage();

               if (stageGen == null)
                   stageGen = Instantiate(m_StageGen, Vector3.zero, Quaternion.identity) as GameObject;

                GameObject obj = Instantiate(m_StartCount, new Vector3(0.0f, 0.0f, 12.0f), m_StartCount.transform.rotation) as GameObject;
                obj.transform.SetParent(m_Camera.transform, false);

                //キョリ、コインテキストを表示
                foreach (Text tex in m_Text_UI)
                {
                    tex.gameObject.SetActive(true);
                }

                GameSceneManager.scene = GameSceneManager.eGameScene.StartCount;
                break;

            //ゲーム開始時のカウントダウン
            case GameSceneManager.eGameScene.StartCount:
                m_TimeCount += Time.deltaTime;

                if (m_TimeCount >= 3.0f)
                {
                    m_TimeCount = 0.0f;
                    GameSceneManager.isGamePlaying = true;
                    GameSceneManager.scene = GameSceneManager.eGameScene.Play;
                }
                break;
            //ゲームプレイ中
            case GameSceneManager.eGameScene.Play:
                if (GameSceneManager.isGameOver)
                {
                    m_TimeCount += Time.deltaTime;
                    if (m_TimeCount >= m_GameOverWait)
                    {
                        m_TimeCount = 0.0f;
                        GameSceneManager.scene = GameSceneManager.eGameScene.GameOver;
                    }
                }
                break;
            //ゲームオーバー時一度だけ
            case GameSceneManager.eGameScene.GameOver:
                GameSceneManager.isGamePlaying = false;

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

                if (stageGen != null)
                    Destroy(stageGen.gameObject);

                GameSceneManager.scene = GameSceneManager.eGameScene.End;
                break;

            case GameSceneManager.eGameScene.End:
                break;
        }
    }
}