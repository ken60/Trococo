﻿using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class GameScene : MonoBehaviour
{
    public enum eGameScene
    {
        LoadTitle = 0,
        Title,
        LoadGame,
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

    void Start()
    {
        m_Canvas = GameObject.Find("Canvas");
        m_FromTitle = false;
        m_Camera = Camera.main;
    }

    void Update()
    {
        //print(m_GameScene);
        switch (m_GameScene)
        {
            case eGameScene.LoadTitle:
                //セーブデータのロード
                GameManager.Instance.LoadGame();
                //ステージの初期化
                m_StageGenerator.InitStage();
                //プレイヤーの初期化
                m_Player.InitPlayer();
                //ブラーを有効化
                m_Blur.enabledBlur = true;
                //ゲームプレイ中のUIを非表示
                m_UI_Text.SetActive(false);
                //タイトルパネルを表示
                GameObject p_main =  Instantiate(m_Panel_Main, m_Panel_Main.transform.position, Quaternion.identity) as GameObject;
                p_main.transform.SetParent(m_Canvas.transform, false);

                m_FromTitle = true;

                m_GameScene = eGameScene.Title;
                break;

            case eGameScene.Title:
                //Title Showing

                break;

            case eGameScene.LoadGame:
                GameSceneManager.Instance.isGameOver = false;
                //初期化
                GameManager.Instance.InitGame();

                //前のシーンがタイトル以外の時
                if (!m_FromTitle)
                {
                    //セーブデータのロード
                    GameManager.Instance.LoadGame();
                    //ステージの初期化
                    m_StageGenerator.InitStage();
                    //プレイヤーの初期化
                    m_Player.InitPlayer();
                }
                m_FromTitle = false;

                //ゲームプレイ中のUIを表示
                m_UI_Text.SetActive(true);

                GameObject obj = Instantiate(m_StartCount, new Vector3(0.0f, 0.0f, 12.0f), Quaternion.identity) as GameObject;
                obj.transform.SetParent(m_Camera.transform, false);


                m_GameScene = eGameScene.WaitCount;
                break;

            case eGameScene.WaitCount:
                //ブラーを無効化
                m_Blur.enabledBlur = false;

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

                //ゲームプレイ中のUIを非表示
                m_UI_Text.SetActive(false);
                //ブラーを有効化
                m_Blur.enabledBlur = true;

                //リザルトパネルを表示
                GameObject panelRes = Instantiate(m_Panel_Result, m_Panel_Result.transform.position, Quaternion.identity) as GameObject;
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