using UnityEngine;
using System.Collections;

public class GameData : SingletonMonoBehaviour<GameData>
{
    private int m_Score = 0;
    private int m_CoinNum = 0;
    private int m_TomatoNum = 0;
    private bool m_isGamePlay = false;
    private bool m_isGameOver = false;

    public void InitGame()
    {
        m_Score = 0;
        m_CoinNum = 0;
        m_TomatoNum = 0;
        m_isGameOver = false;
        m_isGamePlay = false;
    }

    public int Score
    {
        get
        {
            return m_Score;
        }
        set
        {
            m_Score = value;
        }
    }

    public int Coin
    {
        get
        {
            return m_CoinNum;
        }
        set
        {
            m_CoinNum = value;
        }
    }

    public int Tomato
    {
        get
        {
            return m_TomatoNum;
        }
        set
        {
            m_TomatoNum = value;
        }
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

    public bool IsGamePlay
    {
        get
        {
            return m_isGamePlay;
        }
        set
        {
            m_isGamePlay = value;
        }
    }
}