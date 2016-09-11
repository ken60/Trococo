using UnityEngine;

public class GameSceneManager : SingletonMonoBehaviour<GameSceneManager>
{
    [SerializeField]
    private GameObject m_PausePanel;


    private GameObject m_Canvas;
    private bool m_isGamePlaying = false;
    private bool m_isPause = false;
    private bool m_isGameOver = false;
    private bool m_isCollider = true;

    void Start()
    {
        m_Canvas = GameObject.Find("Canvas");
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

    public bool isPause
    {
        get { return m_isPause; }
        set { m_isPause = value; }
    }

    //画面に出たイカスミを削除
    public void DestroySquidInk()
    {
        GameObject[] tagobjs = GameObject.FindGameObjectsWithTag("Squid_ink");
        foreach (GameObject obj in tagobjs)
        {
            Destroy(obj);
        }
    }

    //バックグラウンド移行時
    void OnApplicationPause(bool pauseStatus)
    {
        if (m_isGamePlaying)
        {
            if (pauseStatus)
            {
                GamePause();
            }
        }
    }

    //ポーズ
    public void GamePause()
    {
        GameObject panel = Instantiate(m_PausePanel, Vector3.zero, Quaternion.identity) as GameObject;
        panel.transform.SetParent(m_Canvas.transform, false);
    }
}