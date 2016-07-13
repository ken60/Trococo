using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Panel_Title : MonoBehaviour
{
    private RectTransform m_RectTransform;

    void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();

        //セーブデータのロード
        GameManager.Instance.LoadGame();

        MoveIn();
    }

    void Update()
    {
        //MoveOut();
    }

    void MoveIn()
    {
        Hashtable parameters = new Hashtable();
        parameters.Add("y", Screen.height * 0.5f);
        parameters.Add("easeType", iTween.EaseType.easeOutBounce);
        iTween.MoveTo(gameObject, parameters);
    }

    public void MoveOut()
    {
        Hashtable parameters = new Hashtable();
        parameters.Add("y", -(Screen.height * 0.5f) - m_RectTransform.sizeDelta.y);
        parameters.Add("easeType", iTween.EaseType.easeInOutBack);
        parameters.Add("oncomplete", "DestroyPanel");
        parameters.Add("oncompletetarget", gameObject);
        iTween.MoveTo(gameObject, parameters);
    }

    void DestroyPanel()
    {
        Destroy(gameObject);
    }
}