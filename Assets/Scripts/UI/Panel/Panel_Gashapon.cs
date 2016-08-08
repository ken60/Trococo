using UnityEngine;
using System.Collections;

public class Panel_Gashapon : MonoBehaviour
{
    private RectTransform m_RectTransform;


    void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();
        transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
    }
	
	void Update ()
	{
	
	}
}