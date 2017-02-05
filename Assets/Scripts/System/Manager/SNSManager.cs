using UnityEngine;
using SocialConnector;
using System.Collections;
using System.IO;

public class SNSManager : SingletonMonoBehaviour<SNSManager>
{
    [SerializeField]
    private GameObject m_Dialog;

    private const string m_FileName = "Screenshot.png";
    private string m_ShareText = "";
    private string m_ShareURL_Android = "";
    private string m_ShareURL_iPhone = "";
    private string m_ImagePath = "";

    void Start()
    {
        m_ImagePath = Application.persistentDataPath + "/" + m_FileName;
    }

    public void Share()
    {
        if (File.Exists(m_ImagePath))
        {
            SnsImageShare();
        }
        else
        {
            ImageNotExists();
            SnsShare();
        }
    }

    void SnsImageShare()
    {
        m_ShareText = "[テスト]トロココで" + GameDataManager.Instance.score + "m進んだ！\n";

        if (Application.platform == RuntimePlatform.Android)
        {
            SocialConnector.SocialConnector.Share(m_ShareText, m_ShareURL_Android, m_ImagePath);
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            SocialConnector.SocialConnector.Share(m_ShareText, m_ShareURL_iPhone, m_ImagePath);
        }

    }

    void SnsShare()
    {
        m_ShareText = "[テスト]トロココで" + GameDataManager.Instance.score + "m進んだ！\n";

        if (Application.platform == RuntimePlatform.Android)
        {
            SocialConnector.SocialConnector.Share(m_ShareText, m_ShareURL_Android, null);
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            SocialConnector.SocialConnector.Share(m_ShareText, m_ShareURL_iPhone, null);
        }
    }

    public void CaptureScreenshot()
    {
        StartCoroutine(Screenshot());
    }

    IEnumerator Screenshot()
    {
        if (File.Exists(m_ImagePath))
        {
            File.Delete(m_ImagePath);

            var deletion = new WaitForFile(m_ImagePath, false, 1.0f);
            yield return deletion;

            if (!deletion.IsCompleted)
            {
                // Timeout.
                yield break;
            }
        }

        Application.CaptureScreenshot(m_FileName);

        var creation = new WaitForFile(m_ImagePath, true, 1.0f);
        yield return creation;

        if (!creation.IsCompleted)
        {
            // Timeout.
            yield break;
        }

        // Do something with the file.
    }

    void ImageNotExists()
    {
        GameObject dialog = Instantiate(m_Dialog, Vector3.zero, Quaternion.identity) as GameObject;
        dialog.transform.SetParent(GameObject.Find("Canvas").transform, false);
        dialog.GetComponent<DialogBox>().SetText("スクリーンショットに\n失敗しました...\n\nメッセージのみをシェアします");
    }

    public string fileName
    {
        get { return m_FileName; }
    }
}
