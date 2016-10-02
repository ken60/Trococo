using UnityEngine;
using SocialConnector;

public class SNSManager : SingletonMonoBehaviour<SNSManager>
{
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
}
