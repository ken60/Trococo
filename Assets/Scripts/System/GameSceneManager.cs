using UnityEngine;
using System.Collections;

public class GameSceneManager : MonoBehaviour
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

    private static bool m_GameStart = false;
    private static bool m_HawToPlay = false;
    private static bool m_isGamePlaying = false;
    private static bool m_isGameOver = false;
    private static bool m_isCollider = true;    //Debug only
    private static eGameScene m_Scene;

    public static void InitGame()
    {
        m_Scene = eGameScene.LoadTitle;
        m_GameStart = false;
        m_HawToPlay = false;
        m_isGamePlaying = false;
        m_isGameOver = false;
        m_isCollider = true;    //Debug only
    }

    public static eGameScene scene
    {
        get
        {
            return m_Scene;
        }
        set
        {
            m_Scene = value;
        }
    }


    public static bool isGameOver
    {
        get
        {
            return m_isGameOver;
        }
        set
        {
            m_isGameOver = value;
        }
    }

    public static bool isGamePlaying
    {
        get
        {
            return m_isGamePlaying;
        }
        set
        {
            m_isGamePlaying = value;
        }
    }

    //Debug only
    public static bool colliderEnabled
    {
        get
        {
            return m_isCollider;
        }
        set
        {
            m_isCollider = value;
        }
    }

}