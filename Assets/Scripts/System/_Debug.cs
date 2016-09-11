using UnityEngine;
using UnityEngine.UI;

public class _Debug : MonoBehaviour
{
    private Text m_Text;

    //fps測定用
    int frameCount;
    float prevTime;
    float fps;

    void Start()
    {
        m_Text = GetComponent<Text>();
        frameCount = 0;
        prevTime = 0.0f;
    }

    void Update()
    {
        fpsCount();

        m_Text.text =
            "fps: " + fps.ToString("0.00") + "\n" +
            "loadHighscore: " + GameManager.Instance.highScore + "\n" +
            "loadTotalGoldCoin: " + GameManager.Instance.totalGoldCoinNum;
    }

    void fpsCount()
    {
        ++frameCount;
        float time = Time.realtimeSinceStartup - prevTime;

        if (time >= 0.5f)
        {
            fps = frameCount / time;

            frameCount = 0;
            prevTime = Time.realtimeSinceStartup;
        }
    }
}