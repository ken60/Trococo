using UnityEngine;
using System.Collections;

public class GameSceneManager : SingletonMonoBehaviour<GameSceneManager>
{
    private bool m_GameStart = false;
    private bool m_HawToPlay = false;
    private bool m_isGamePlaying = false;
    private bool m_isGameOver = false;
    private bool m_isCollider = true;

    public void InitGame()
    {
        m_GameStart = false;
        m_HawToPlay = false;
        m_isGamePlaying = false;
        m_isGameOver = false;
    }

    public bool isGameOver
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

    public bool isGamePlaying
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

    public bool colliderEnabled
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