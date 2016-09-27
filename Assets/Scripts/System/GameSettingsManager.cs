using UnityEngine;
using System.Collections;

[System.Serializable]
class SettingData
{
    public bool isAudioEnabled = false;  //音を有効にするか
    public bool isEnableShadow = true;  //影を有効にするか
    public bool isUserRegisterCompletion = false;
    public string userID = "";
    public string password = "";
}

public class GameSettingsManager : SingletonMonoBehaviour<GameSettingsManager>
{
    [SerializeField]
    private string m_FileName;

    private bool m_isAudioEnabled = false;  //音を有効にするか
    private bool m_isAudioEnabled_old = false;
    private bool m_isShadowEnabled = true;  //影を有効にするか
    private bool m_isShadowEnabled_old = true;
    private bool m_isUserRegisterCompletion = false; //ユーザー登録が完了しているか
    private string m_UserID = "";
    private string m_Password = "";

    private string m_Json;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadSettings()
    {
        SettingData loadData = JsonUtility.FromJson(FileIO.FileRead(m_FileName), typeof(SettingData)) as SettingData;
        if (loadData == null) return;

        //Settings
        m_isAudioEnabled = loadData.isAudioEnabled;
        m_isAudioEnabled_old = loadData.isAudioEnabled;
        m_isShadowEnabled = loadData.isEnableShadow;
        m_isShadowEnabled_old = loadData.isEnableShadow;

        m_isUserRegisterCompletion = loadData.isUserRegisterCompletion;

        m_UserID = loadData.userID;
        m_Password = loadData.password;
    }

    public void SaveSettings()
    {
        CheckUserRegisterCompletion();

        SettingData data = new SettingData();

        //Settings
        data.isAudioEnabled = m_isAudioEnabled;
        data.isEnableShadow = m_isShadowEnabled;

        data.isUserRegisterCompletion = m_isUserRegisterCompletion;

        data.userID = m_UserID;
        data.password = m_Password;

        m_Json = JsonUtility.ToJson(data);
        FileIO.FileWrite(m_FileName, m_Json);

        //Oldを更新
        m_isAudioEnabled_old = m_isAudioEnabled;
        m_isShadowEnabled_old = m_isShadowEnabled;
    }

    void CheckUserRegisterCompletion()
    {
        print(m_UserID + m_Password);

        if (m_UserID != "" && m_Password != "")
        {
            print("m_isUserRegisterCompletion = true");
            m_isUserRegisterCompletion = true;
        }
    }

    //音をミュート
    public bool isAudioEnabled
    {
        get { return m_isAudioEnabled; }
        set { m_isAudioEnabled = value; }
    }

    //影の有無
    public bool isShadowEnable
    {
        get { return m_isShadowEnabled; }
        set { m_isShadowEnabled = value; }
    }

    //ユーザー名
    public string userID
    {
        get { return m_UserID; }
        set { m_UserID = value; }
    }

    //パスワード
    public string password
    {
        get { return m_Password; }
        set { m_Password = value; }
    }

    //設定を変更したか
    public bool isChangingSettings()
    {
        if (m_isAudioEnabled != m_isAudioEnabled_old)
            return true;

        else if (m_isShadowEnabled != m_isShadowEnabled_old)
            return true;

        return false;
    }

    //ユーザー登録が完了しているか
    public bool isUserRegisterCompletion
    {
        get { return m_isUserRegisterCompletion; }
    }
}