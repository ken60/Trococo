using UnityEngine;
using UnityEngine.UI;

public class GameSceneManager : SingletonMonoBehaviour<GameSceneManager>
{
    [SerializeField]
    private GameObject m_PausePanel;
    [SerializeField]
    private Button m_ButtonPause;
    
    private GameObject m_Canvas;
    private bool m_isStartCount = false;
    private bool m_isGamePlaying = false;
    private bool m_isPause = false;
    private bool m_isGameOver = false;

    void Start()
    {
        m_Canvas = GameObject.Find("Canvas");
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (!m_isGamePlaying)
            m_ButtonPause.interactable = false;
        else
            m_ButtonPause.interactable = true;
    }

    public void InitGame()
    {
        m_isGamePlaying = false;
        m_isGameOver = false;
    }

    public bool isStartCount
    {
        get { return m_isStartCount; }
        set { m_isStartCount = value; }
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

    public bool isGameOver
    {
        get { return m_isGameOver; }
        set { m_isGameOver = value; }
    }

    //画面に出たエフェクトを削除
    public void DestroyEffect()
    {
        GameObject[] ink = GameObject.FindGameObjectsWithTag("Squid_ink");
        if (ink != null)
            foreach (GameObject obj in ink)
                Destroy(obj);

        GameObject[] Smoke = GameObject.FindGameObjectsWithTag("Smoke");
        if (Smoke != null)
            foreach (GameObject obj in Smoke)
                Destroy(obj);
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
        if (isStartCount) return;

        GameObject panel = Instantiate(m_PausePanel, Vector3.zero, Quaternion.identity) as GameObject;
        panel.transform.SetParent(m_Canvas.transform, false);
    }
}