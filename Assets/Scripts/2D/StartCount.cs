using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartCount : MonoBehaviour
{
    [SerializeField]
    private Sprite[] m_NumberSprite;

    private Image m_CountImage;
    private float m_TimeCount = 0.0f;

    void Start()
    {
        GameSceneManager.Instance.isStartCount = true;
        m_CountImage = GetComponent<Image>();
        transform.localScale = Vector3.zero; 
        iTweenManager.Show_ScaleTo(this.gameObject, 0.15f);
    }

    void Update()
    {
        m_TimeCount += Time.deltaTime;
        //3
        if (m_TimeCount < 1.0f)
        {
            m_CountImage.sprite = m_NumberSprite[0];
        }
        //2
        else if (m_TimeCount < 2.0f)
        {
            m_CountImage.sprite = m_NumberSprite[1];

        }
        //1
        else if (m_TimeCount < 3.0f)
        {
            m_CountImage.sprite = m_NumberSprite[2];
        }
        //GO!
        else if (m_TimeCount < 3.5f)
        {
            m_CountImage.sprite = m_NumberSprite[3];

            GameSceneManager.Instance.isGamePlaying = true;
            GameScene.m_GameScene = GameScene.eGameScene.Play;
        }
        else
        {
            iTweenManager.Hide_ScaleTo(this.gameObject, 0.2f, "EndAction", this.gameObject);
        }
    }

    void EndAction()
    {
        GameSceneManager.Instance.isStartCount = false;
        Destroy(this.gameObject);
    }
}