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
        transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        iTweenManager.Show_ScaleTo(this.gameObject, 0.3f);
    }

    public void Button_Next()
    {
        if (m_PageNumber < m_TutorialSprite.Length - 1)
        {
            m_PageNumber++;
            iTweenManager.Hide_ScaleTo(this.gameObject, 0.1f, "EndAction", this.gameObject);
        }
        else
        {
            iTweenManager.Hide_ScaleTo(this.gameObject, 0.1f, "End", this.gameObject);
        }
    }

    void EndAction()
    {
        m_Image.sprite = m_TutorialSprite[m_PageNumber];

        if (m_PageNumber == m_TutorialSprite.Length - 1)
            m_ButtonText.text = "スタート";

        iTweenManager.Show_ScaleTo(this.gameObject, 0.3f);
    }

    void End()
    {
        m_isEnd = true;
    }
}