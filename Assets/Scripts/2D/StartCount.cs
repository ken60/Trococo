using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartCount : MonoBehaviour
{
    [SerializeField]
    private Sprite[] m_NumberSprite;

    private Image m_CountImage;
    private int m_ImageCount = 0;
    private float m_TimeCount = 0.0f;

    void Start()
    {
        m_CountImage = GetComponent<Image>();
        transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        Show();
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
            Hide();
        }
    }

    void Show()
    {
        Hashtable hash = new Hashtable();
        hash.Add("x", 1.0f);
        hash.Add("y", 1.0f);
        hash.Add("time", 0.15f);
        hash.Add("easeType", iTween.EaseType.easeInOutSine);
        iTween.ScaleTo(this.gameObject, hash);
    }

    void Hide()
    {
        Hashtable hash = new Hashtable();
        hash.Add("x", 0.0f);
        hash.Add("y", 0.0f);
        hash.Add("time", 0.2f);
        hash.Add("easeType", iTween.EaseType.easeInOutSine);
        hash.Add("oncomplete", "EndAction");
        hash.Add("oncompletetarget", this.gameObject);
        iTween.ScaleTo(this.gameObject, hash);
    }

    void EndAction()
    {
        Destroy(this.gameObject);
    }
}