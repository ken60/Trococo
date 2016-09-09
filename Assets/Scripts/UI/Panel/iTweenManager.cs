using UnityEngine;
using System.Collections;

public class iTweenManager : MonoBehaviour
{
    public static void Show_ScaleTo(GameObject obj, float time)
    {
        Hashtable hash = new Hashtable();
        hash.Add("x", 1.0f);
        hash.Add("y", 1.0f);
        hash.Add("time", time);
        hash.Add("easeType", iTween.EaseType.easeInOutSine);
        iTween.ScaleTo(obj.gameObject, hash);
    }

    public static void Hide_ScaleTo(GameObject obj, float time, string oncomplete, GameObject oncompletetarget)
    {
        Hashtable hash = new Hashtable();
        hash.Add("x", 0.0f);
        hash.Add("y", 0.0f);
        hash.Add("time", time);
        hash.Add("easeType", iTween.EaseType.easeInOutSine);
        hash.Add("oncomplete", oncomplete);
        hash.Add("oncompletetarget", oncompletetarget.gameObject);
        iTween.ScaleTo(obj.gameObject, hash);
    }

    public static void MoveOut_MoveTo_Y(GameObject obj, float time, float y, string oncomplete, GameObject oncompletetarget)
    {
        Hashtable hash = new Hashtable();
        hash.Add("y", y);
        hash.Add("time", time);
        hash.Add("easeType", iTween.EaseType.easeInOutSine);
        hash.Add("oncomplete", oncomplete);
        hash.Add("oncompletetarget", oncompletetarget.gameObject);
        iTween.MoveTo(obj.gameObject, hash);
    }

}