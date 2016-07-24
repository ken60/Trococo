using UnityEngine;
using System.Collections;

public class Panel_CharSelect : MonoBehaviour
{
	void Start ()
	{
        MoveIn();

    }
	
	void Update ()
	{
	
	}

    //iTween
    void MoveIn()
    {
        Hashtable parameters = new Hashtable();
        parameters.Add("y", Screen.height * 0.5f);
        parameters.Add("easeType", iTween.EaseType.easeInOutBack);
        iTween.MoveTo(gameObject, parameters);
    }
}