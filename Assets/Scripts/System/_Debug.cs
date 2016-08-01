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
            "fps: " + fps.ToString("0.00") + "\n" +
            "loadHighscore: " + GameManager.Instance.highScore + "\n" +
            "loadTotalGoldCoin: " + GameManager.Instance.totalGoldCoinNum + "\n" +
            "loadTotalCopperCoin: " + GameManager.Instance.totalCopperCoinNum + "\n" +
            "colliderEnabled: " + GameSceneManager.Instance.colliderEnabled + "\n" +
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
        GameSceneManager.Instance.colliderEnabled = !GameSceneManager.Instance.colliderEnabled;
    }
}