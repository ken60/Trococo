using UnityEngine;

[System.Serializable]
class SettingData
{
    public bool isAudioEnabled = true;  //音を有効にするか
    public bool isEnableShadow = true;  //影を有効にするか
}

public class SettingManager : SingletonMonoBehaviour<SettingManager>
{
    [SerializeField]
    private string m_FileName = "trd00003";

    private bool m_isAudioEnabled = true;  //音を有効にするか
    private bool m_isAudioEnabled_old = true;
    private bool m_isShadowEnabled = true;  //影を有効にするか
    private bool m_isShadowEnabled_old = true;

    private string m_Json;

    public void LoadSetting()
    {
        SettingData loadData = JsonUtility.FromJson(FileIO.FileRead(m_FileName), typeof(SettingData)) as SettingData;
        if (loadData == null) return;

        //Settings
        m_isAudioEnabled = loadData.isAudioEnabled;
        m_isAudioEnabled_old = loadData.isAudioEnabled;
        m_isShadowEnabled = loadData.isEnableShadow;
        m_isShadowEnabled_old = loadData.isEnableShadow;
    }

    public void SaveSetting()
    {
        SettingData data = new SettingData();

        //Settings
        data.isAudioEnabled = m_isAudioEnabled;
        data.isEnableShadow = m_isShadowEnabled;

        m_isAudioEnabled_old = m_isAudioEnabled;
        m_isShadowEnabled_old = m_isShadowEnabled;

        m_Json = JsonUtility.ToJson(data);
        FileIO.FileWrite(m_FileName, m_Json);
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

    //設定を変更したか
    public bool isChangingSettings()
    {
        if (m_isAudioEnabled != m_isAudioEnabled_old)
            return true;

        else if (m_isShadowEnabled != m_isShadowEnabled_old)
            return true;

        return false;
    }
}