using UnityEngine;
using System.Collections;

public class GameSceneManager : SingletonMonoBehaviour<GameSceneManager>
{
    private bool m_GameStart = false;
    private bool m_HawToPlay = false;
    private bool m_isGamePlaying = false;
    private bool m_isGameOver = false;

    public void InitGame()
    {
        m_isGameOver = false;
        m_isGamePlaying = false;
    }

    public bool IsGameOver
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

    public bool IsGamePlaying
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
}