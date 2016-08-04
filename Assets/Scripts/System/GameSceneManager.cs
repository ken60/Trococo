using UnityEngine;

public class GameSceneManager : SingletonMonoBehaviour<GameSceneManager>
{
    private bool m_isGamePlaying = false;
    private bool m_isGameOver = false;
    private bool m_isCollider = true;

    public void InitGame()
    {
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

    //Debug only
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