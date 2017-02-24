using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRC_Utility : MonoBehaviour
{
    private static GameObject canvas;

    void Awake()
    {
        canvas = GameObject.Find("Canvas");

        if (canvas == null) Debug.LogError("Canvas is Null");
    }

    public static GameObject CanvasInstantilate(GameObject original, Vector3 potision, Quaternion rotation)
    {
        GameObject g = Instantiate(original, potision, rotation) as GameObject;
        g.transform.SetParent(canvas.transform, false);
        return g;
    }
}
