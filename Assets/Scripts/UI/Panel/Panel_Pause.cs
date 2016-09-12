using UnityEngine;

public class Panel_Pause : MonoBehaviour
{
    [SerializeField]
    private GameObject m_StartCount;

    private GameObject m_Canvas;

    void Start()
    {
        m_Canvas = GameObject.Find("Canvas");
        Time.timeScale = 0.0f;
        GameSceneManager.Instance.isPause = true;
        transform.localScale = Vector3.zero;
        iTweenManager.Show_ScaleTo(this.gameObject, 0.2f);
    }

    //つづける
    public void Resumption()
    {
        iTweenManager.Hide_ScaleTo(this.gameObject, 0.35f, "EndAnimResumption", this.gameObject);
        GameSceneManager.Instance.isPause = false;
    }

    //ホームへ
    public void BackToHome()
    {
        Time.timeScale = 1.0f;
        iTweenManager.Hide_ScaleTo(this.gameObject, 0.35f, "EndAnimBackToHome", this.gameObject);
        GameSceneManager.Instance.isPause = false;
        GameSceneManager.Instance.isGamePlaying = false;
        GameSceneManager.Instance.DestroyEffect();
        GameScene.m_GameScene = GameScene.eGameScene.LoadTitle;
    }

    void EndAnimResumption()
    {
        Time.timeScale = 1.0f;
    }

    void EndAnimBackToHome()
    {
        Destroy(this.gameObject);
    }
}