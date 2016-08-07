using UnityEngine;
using System.Collections;

public class Panel_Gashapon : MonoBehaviour
{
    private RectTransform m_RectTransform;


    void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();
    }
	
	void Update ()
	{
	
	}

    //********iTween********
    void MoveIn(GameObject gameObj, float delay)
    {
        Hashtable parameters = new Hashtable();
        parameters.Add("x", Screen.width * 0.5f);
        parameters.Add("time", 0.4f);
        parameters.Add("delay", delay);
        parameters.Add("easeType", iTween.EaseType.easeInOutSine);

        iTween.MoveTo(gameObj, parameters);
    }
    
    void MoveOut()
    {
        Hashtable parameters = new Hashtable();
        parameters.Add("y", -Screen.height + m_RectTransform.sizeDelta.y);
        parameters.Add("time", 0.4f);
        parameters.Add("easeType", iTween.EaseType.easeInOutSine);
        parameters.Add("oncomplete", "Destroy");
        parameters.Add("oncompletetarget", gameObject);
        iTween.MoveTo(gameObject, parameters);
    }
}