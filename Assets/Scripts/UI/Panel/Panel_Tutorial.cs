using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Panel_Tutorial : MonoBehaviour
{
    public bool m_isEnd = false;

    [SerializeField]
    private Sprite[] m_TutorialSprite = null;
    [SerializeField]
    private Image m_Image = null;
    [SerializeField]
    private Text m_ButtonText = null;

    private int m_PageNumber = 0;

    void Start()
    {
        m_Image.sprite = m_TutorialSprite[m_PageNumber];
        transform.localScale = new Vector3(0.0f, 0.0f, 0.0f); PanelShow(this.gameObject);
    }

    void Update()
    {
        if (m_PageNumber == m_TutorialSprite.Length - 1)
        {
            m_ButtonText.text = "スタート";
        }
    }

    public void Button_Next()
    {
        if (m_PageNumber < m_TutorialSprite.Length - 1)
        {
            m_PageNumber++;
            PanelHide(this.gameObject);
        }
        else
        {
            PanelHideDestroy(this.gameObject);
        }
    }

    //********iTween********
    void PanelShow(GameObject panel)
    {
        Hashtable hash = new Hashtable();
        hash.Add("x", 1.0f);
        hash.Add("y", 1.0f);
        hash.Add("time", 0.4f);
        hash.Add("easeType", iTween.EaseType.easeInOutSine);
        iTween.ScaleTo(panel.gameObject, hash);
    }

    void PanelHide(GameObject panel)
    {
        Hashtable hash = new Hashtable();
        hash.Add("x", 0.0f);
        hash.Add("y", 0.0f);
        hash.Add("time", 0.2f);
        hash.Add("easeType", iTween.EaseType.easeInOutSine);
        hash.Add("oncomplete", "EndAction");
        hash.Add("oncompletetarget", this.gameObject);
        iTween.ScaleTo(panel.gameObject, hash);
    }

    void PanelHideDestroy(GameObject panel)
    {
        Hashtable hash = new Hashtable();
        hash.Add("x", 0.0f);
        hash.Add("y", 0.0f);
        hash.Add("time", 0.2f);
        hash.Add("easeType", iTween.EaseType.easeInOutSine);
        hash.Add("oncomplete", "End");
        hash.Add("oncompletetarget", this.gameObject);
        iTween.ScaleTo(panel.gameObject, hash);
    }

    void EndAction()
    {
        m_Image.sprite = m_TutorialSprite[m_PageNumber];
        PanelShow(this.gameObject);
    }

    void End()
    {
        m_isEnd = true;
    }
}