using UnityEngine;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
    public enum eGameScene
    {
        LoadTitle = 0,
        Title,
        LoadGame,
        Tutorial,
        WaitCount,
        StartCount,
        Play,
        GameOver,
        End
    }

    [HideInInspector]
    static public eGameScene m_GameScene;

    [SerializeField]
    private GameObject m_UI_Text;   //ゲーム中のスコア表示Text
    [SerializeField]
    private GameObject m_Panel_Signin;  //サインインパネル
    [SerializeField]
    private GameObject m_Panel_Main;  //メインパネルスクリプト
    [SerializeField]
    private GameObject m_Panel_Result;  //リザルトパネル
    [SerializeField]
    private GameObject m_Panel_Tutorial; //チュートリアルパネル
    [SerializeField]
    private GameObject m_StartCount;    //スタートカウントダウン
    [SerializeField]
    private Player m_Player;    //プレイヤースクリプト プレイヤー初期化で必要
    [SerializeField]
    private StageGenerator m_StageGenerator;    //ステージ生成スクリプト ステージ初期化で必要
    [SerializeField]
    private Blur m_Blur;            //ブラースクリプト
    [SerializeField]
    private float m_GameOverWait;   //ゲームオーバー時の待機時間

    private float m_TimeCount = 0.0f;
    private bool m_FromTitle = false;   //タイトルからの遷移か
    private bool m_isLoggingIn = false;
    private bool m_CaptureSS = false;   //スクリーンショットを撮ったか
    private GameObject m_TutorialPanel;

    void Awake()
    {
        AccountManager.Instance.LoadAccount();
        SettingManager.Instance.LoadSetting();
    }

    void Start()
    {
        m_FromTitle = false;

        //ネットワーク接続確認
        NetworkChecker.Instance.ReachableCheck();

        if (AccountManager.Instance.userName == "" && !m_isLoggingIn)
        {
            //サインインパネルを表示
            TRC_Utility.CanvasInstantilate(m_Panel_Signin, m_Panel_Signin.transform.position, Quaternion.identity);
        }
        else
        {
            //ログイン
            NCMBManager.Instance.Login(AccountManager.Instance.userName, AccountManager.Instance.userPass);
            m_isLoggingIn = true;
        }
    }

    void Update()
    {
        switch (m_GameScene)
        {
            case eGameScene.LoadTitle:
                //セーブデzータのロード
                GameDataManager.Instance.LoadGame();
                //設定の適用
                ReflectSettings();
                //ステージの初期化
                m_StageGenerator.InitStage();
                //プレイヤーの初期化
                m_Player.InitPlayer();
                //ブラーを有効化
                m_Blur.enabled = true;
                //ゲームプレイ中のUIを非表示
                m_UI_Text.SetActive(false);
                //タイトルパネルを表示
                GameObject main = TRC_Utility.CanvasInstantilate(m_Panel_Main, m_Panel_Main.transform.position, Quaternion.identity) as GameObject;
                iTweenManager.Show_ScaleTo(main.gameObject, 0.35f);

                m_FromTitle = true;
                m_CaptureSS = false;

                m_GameScene = eGameScene.Title;

                break;

            case eGameScene.Title:
                //Title Showing
                //Panel_MainのLoadGame()にシーンチェンジ記述

                break;

            case eGameScene.LoadGame:
                GameSceneManager.Instance.isGameOver = false;
                //初期化
                GameDataManager.Instance.InitGame();

                //前のシーンがタイトル以外の時
                if (!m_FromTitle)
                {
                    //セーブデータのロード
                    GameDataManager.Instance.LoadGame();
                    //ステージの初期化
                    m_StageGenerator.InitStage();
                    //プレイヤーの初期化
                    m_Player.InitPlayer();
                }
                m_FromTitle = false;

                //ゲームプレイ中のUIを表示
                m_UI_Text.SetActive(true);

                //ブラーを無効化
                m_Blur.enabled = false;

                //初回起動時チュートリアル表示
                if (GameDataManager.Instance.isFerstStart)
                {
                    TRC_Utility.CanvasInstantilate(m_Panel_Tutorial, Vector3.zero, Quaternion.identity);

                    m_GameScene = eGameScene.Tutorial;
                }
                else
                {
                    m_GameScene = eGameScene.WaitCount;
                }

                break;

            case eGameScene.Tutorial:

                if (m_TutorialPanel.GetComponent<Panel_Tutorial>().m_isEnd)
                {
                    Destroy(m_TutorialPanel.gameObject);
                    GameDataManager.Instance.isFerstStart = false;
                    GameDataManager.Instance.SaveGame();
                    m_GameScene = eGameScene.WaitCount;
                }

                break;

            case eGameScene.WaitCount:

                //カウントダウンを表示
                TRC_Utility.CanvasInstantilate(m_StartCount, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);

                GameDataManager.Instance.SaveGame();

                m_GameScene = eGameScene.StartCount;
                break;

            //ゲーム開始時のカウントダウン
            case eGameScene.StartCount:
                //StartCountのUpdate()にシーンチェンジ記述

                break;

            //ゲームプレイ中
            case eGameScene.Play:
                if (GameSceneManager.Instance.isGameOver)
                {
                    //スクリーンショットを撮る
                    if (!m_CaptureSS)
                    {
                        //スクリーンショット
                        SNSManager.Instance.CaptureScreenshot();
                        m_CaptureSS = true;
                    }

                    //Wait
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

                //ゲームプレイ中のUIを非表示
                m_UI_Text.SetActive(false);

                //ブラーを有効化
                m_Blur.enabled = true;

                //リザルトパネルを表示
                TRC_Utility.CanvasInstantilate(m_Panel_Result, m_Panel_Result.transform.position, Quaternion.identity);

                //ハイスコアの時 & Androidのみスコアを送信
                if (GameDataManager.Instance.IsHighScore())// && Application.platform == RuntimePlatform.Android)
                {
                    //ネットワーク接続確認
                    NetworkChecker.Instance.ReachableCheck();

                    //ランキングスコア送信
                    NCMBManager.Instance.SendScore(GameDataManager.Instance.score);
                    print("Send score");
                }

                //セーブ
                GameDataManager.Instance.SaveGame();

                m_GameScene = eGameScene.End;
                break;

            case eGameScene.End:
                break;
        }
    }

    void ReflectSettings()
    {
        //影
        LightManager.Instance.ShadowEnabled(SettingManager.Instance.isShadowEnable);

        //オーディオミュート
        AudioManager.Instance.AudioMute(SettingManager.Instance.isAudioEnabled);
    }
}