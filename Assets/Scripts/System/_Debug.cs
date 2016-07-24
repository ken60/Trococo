using UnityEngine;
using UnityEngine.UI;

public class _Debug : MonoBehaviour
{
    [SerializeField]
    private Text m_Text;
    [SerializeField]
    private Slider m_Slider;

    //fps測定用
    int frameCount;
    float prevTime;
    float fps;

    void Start()
    {
        frameCount = 0;
        prevTime = 0.0f;
    }

    void Update()
    {
        fpsCount();

        m_Text.text =
            Profiler.usedHeapSize / 1048576 + " / " + SystemInfo.systemMemorySize + " MB\n" +
            "fps: " + fps.ToString("0.00") + "\n" +
            "loadHighscore: " + GameManager.Instance.highScore + "\n" +
            "loadTotalCoin: " + GameManager.Instance.totalCoinNum + "\n" +
            "colliderEnabled: " + GameSceneManager.colliderEnabled + "\n" +
            "TapRange: " + m_Slider.value.ToString("0");
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

    public void OnButton()
    {
        GameSceneManager.colliderEnabled = !GameSceneManager.colliderEnabled;
    }
}