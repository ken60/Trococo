using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Button : MonoBehaviour
{
    public void Button_GameStart()
    {
        SceneManager.LoadScene("Game");
    }

    public void Button_HowToPlay()
    {

    }

    public void Button_Ranking()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            RankParkInterface.Instance().StartActivity();
        }
    }
}