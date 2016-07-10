using UnityEngine;
using System.Collections;
public class ConnectRankPark : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }
    public void ButtonClick()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            RankParkInterface.Instance().StartActivity();
        }
    }
}