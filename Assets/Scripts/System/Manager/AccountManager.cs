using UnityEngine;

[System.Serializable]
class AccountData
{
    public string un = "";
    public string ps = "";
}

public class AccountManager : SingletonMonoBehaviour<AccountManager>
{
    [SerializeField]
    private string m_FileName = "trd00001";

    private string m_un = "";
    private string m_ps = "";

    private string m_Json;
    
    public void LoadAccount()
    {
        AccountData loadData = JsonUtility.FromJson(FileIO.FileRead(m_FileName), typeof(AccountData)) as AccountData;
        if (loadData == null) return;

        //Account
        m_un = loadData.un;
        m_ps = loadData.ps;
    }

    public void SaveAccount()
    {
        AccountData data = new AccountData();

        //Account
        data.un = m_un;
        data.ps = m_ps;

        m_Json = JsonUtility.ToJson(data);
        FileIO.FileWrite(m_FileName, m_Json);
    }

    public string userName
    {
        get { return m_un; }
        set { m_un = value; }
    }

    public string userPass
    {
        get { return m_ps; }
        set { m_ps = value; }
    }
}
