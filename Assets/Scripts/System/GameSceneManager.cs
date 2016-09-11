using UnityEngine;

public class GameSceneManager : SingletonMonoBehaviour<GameSceneManager>
{
    [SerializeField]
    private GameObject m_PausePanel;

    private bool m_isGamePlaying = false;
    private bool m_isGameOver = false;
    private bool m_isCollider = true;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void InitGame()
    {
        m_isGamePlaying = false;
        m_isGameOver = false;
    }

    public bool isGameOver
    {
        get { return m_isGameOver; }
        set { m_isGameOver = value; }
    }

    public bool isGamePlaying
    {
        get { return m_isGamePlaying; }
        set { m_isGamePlaying = value; }
    }

    //バックグラウンド移行時
    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            Debug.Log("applicationWillResignActive or onPause");
        }
    }
}