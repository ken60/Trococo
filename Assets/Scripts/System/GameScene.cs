﻿using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

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
    private GameObject m_Panel_Main;  //メインパネルスクリプト
    [SerializeField]
    private GameObject m_Panel_Result;  //リザルトパネル
    [SerializeField]
    private GameObject m_Panel_Tutorial; //チュートリアルパネル
    [SerializeField]
    private GameObject m_Dialog_UserRegister;   //ユーザー登録パネル
    [SerializeField]
    private GameObject m_StartCount;    //スタートカウントダウン
    [SerializeField]
    private Player m_Player;    //プレイヤースクリプト プレイヤー初期化で必要
    [SerializeField]
    private StageGenerator m_StageGenerator;    //ステージ生成スクリプト ステージ初期化で必要
    [SerializeField]
    private BlurOptimized m_Blur;   //StandardAsset Blur
    [SerializeField]
    private float m_GameOverWait;   //ゲームオーバー時の待機時間

    private Camera m_Camera;
    private GameObject m_Canvas;
    private float m_TimeCount = 0.0f;
    private bool m_FromTitle = false;   //タイトルからの遷移か
    private GameObject m_TutorialPanel;

    void Awake()
    {
        GameDataManager.Instance.LoadGame();
    }

    void Start()
    {
        m_Canvas = GameObject.Find("Canvas");
        m_FromTitle = false;
        m_Camera = Camera.main;
    }

    void Update()
    {
        switch (m_GameScene)
        {
            case eGameScene.LoadTitle:
                //セーブデータのロード
                GameDataManager.Instance.LoadGame();
                //設定の適用
                ReflectSettings();
                //ステージの初期化
                m_StageGenerator.InitStage();
                //プレイヤーの初期化
                m_Player.InitPlayer();
                //ブラーを有効化
                m_Blur.enabledBlur = true;
                //ゲームプレイ中のUIを非表示
                m_UI_Text.SetActive(false);
                //タイトルパネルを表示
                GameObject main = Instantiate(m_Panel_Main, m_Panel_Main.transform.position, Quaternion.identity) as GameObject;
                main.transform.SetParent(m_Canvas.transform, false);
                iTweenManager.Show_ScaleTo(main.gameObject, 0.35f);

                m_FromTitle = true;

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

                //初回起動時チュートリアル表示
                if (GameDataManager.Instance.isFerstStart)
                {
                    m_TutorialPanel = Instantiate(m_Panel_Tutorial, Vector3.zero, Quaternion.identity) as GameObject;
                    m_TutorialPanel.transform.SetParent(m_Canvas.transform, false);

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
                //ブラーを無効化
                m_Blur.enabledBlur = false;

                //カウントダウンを表示
                GameObject count = Instantiate(m_StartCount, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
                count.transform.SetParent(m_Canvas.transform, false);

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
                m_Blur.enabledBlur = true;

                //リザルトパネルを表示
                GameObject panelRes = Instantiate(m_Panel_Result, m_Panel_Result.transform.position, Quaternion.identity) as GameObject;
                panelRes.transform.SetParent(m_Canvas.transform, false);

                //ハイスコアの時 & Androidのみスコアを送信
                if (GameDataManager.Instance.IsHighScore() && Application.platform == RuntimePlatform.Android)
                {
                    //ランキングスコア送信
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
        LightManager.Instance.ShadowEnabled(GameDataManager.Instance.isShadowEnable);

        //オーディオミュート
        AudioManager.Instance.AudioMute(GameDataManager.Instance.isAudioEnabled);
    }
}