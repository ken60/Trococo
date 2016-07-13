using UnityEngine;
using System.Collections;

public class RankParkInterface
{
#if UNITY_ANDROID
    private static RankParkInterface _instance = null;

    private AndroidJavaClass rankPark = null;
    private AndroidJavaObject mainActivity = null;

    public RankParkInterface()
    {
        rankPark = new AndroidJavaClass("com.rankpark.RankPark");
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        mainActivity = jc.GetStatic<AndroidJavaObject>("currentActivity");
    }

    public static RankParkInterface Instance()
    {
        if (_instance == null)
            _instance = new RankParkInterface();
        return _instance;
    }

    public void AddScore(int score)
    {
        SafeCall("sendScore", mainActivity, score);
    }

    public void AddScore(float score)
    {
        SafeCall("sendScore", mainActivity, score);
    }

    public void StartActivity()
    {
        SafeCall("startActivity", mainActivity);
    }


    private void SafeCall(string method, params object[] args)
    {
        if (rankPark != null)
        {
            rankPark.CallStatic(method, args);
        }
        else
        {
            Debug.LogError("rankPark is not created");
        }
    }
#endif
}