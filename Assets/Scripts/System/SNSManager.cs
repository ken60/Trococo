using UnityEngine;
using SocialConnector;
using System.Collections;
using System.IO;

public class SNSManager : SingletonMonoBehaviour<SNSManager>
{
    private string m_FileName = "Screenshot.png";
    private string m_ShareText = "";
    private string m_ShareURL = "https://twitter.com/senna_niconico";
    private string m_ImagePath = "";

    public void SnsImageShare()
    {
        m_ShareText = "[テスト]トロココで" + GameDataManager.Instance.score + "m進んだ！\n";
        m_ImagePath = Application.persistentDataPath + "/Twitter.png";

        if (Application.platform == RuntimePlatform.Android)
        {
            SocialConnector.SocialConnector.Share(m_ShareText, m_ShareURL, m_ImagePath);
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            SocialConnector.SocialConnector.Share(m_ShareText, m_ShareURL, m_ImagePath);
        }
    }

    public void SnsShare()
    {
        m_ShareText = "[テスト]トロココで" + GameDataManager.Instance.score + "m進んだ！";
        m_ImagePath = Application.persistentDataPath + "/Twitter.png";

        if (Application.platform == RuntimePlatform.Android)
        {
            SocialConnector.SocialConnector.Share(m_ShareText, m_ShareURL, null);
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            SocialConnector.SocialConnector.Share(m_ShareText, m_ShareURL, null);
        }
    }

    public void CaptureScreenshot(string fileName)
    {
        StartCoroutine(Screenshot(fileName));
    }

    IEnumerator Screenshot(string fileName)
    {
        // スクリーンショットをとる
        Application.CaptureScreenshot(fileName);

        // インジケーター表示
#if UNITY_IPHONE
		Handheld.SetActivityIndicatorStyle(iOSActivityIndicatorStyle.White);
#elif UNITY_ANDROID
        Handheld.SetActivityIndicatorStyle(AndroidActivityIndicatorStyle.Small);
#endif
        Handheld.StartActivityIndicator();

        // スクリーンショットが保存されるまで待機
        long filesize = 0;
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        while (filesize == 0)
        {
            yield return null;

            //ファイルのサイズを取得
            FileInfo fi = new FileInfo(filePath);
            if (fi != null)
            {
                filesize = fi.Length;
            }
        }

        while (!File.Exists(filePath))
        {
            yield return null;
        }

        // インジケーター非表示
        Handheld.StopActivityIndicator();

    }

    public string fileName
    {
        get { return m_FileName; }
    }
}
