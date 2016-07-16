using UnityEngine;

[System.Serializable]
class Data
{
    public int HighScore = 0;
    public int TotalCoinNum = 0;
}

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    private int m_Score = 0;
    private int m_CoinNum = 0;
    private int m_HighScore = 0;
    private int m_OldHighScore = 0;
    private int m_TotalCoinNum = 0;
    private int m_TomatoNum = 0;
    private string m_Json;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void InitGame()
    {
        m_Score = 0;
        m_CoinNum = 0;
        m_HighScore = 0;
        m_OldHighScore = 0;
        m_TotalCoinNum = 0;
        m_TomatoNum = 0;
    }

    public void LoadGame()
    {
        Data loadData = JsonUtility.FromJson(FileIO.FileRead("SaveData"), typeof(Data)) as Data;
        if (loadData == null) return;

        m_HighScore = loadData.HighScore;
        m_OldHighScore = loadData.HighScore;
        m_TotalCoinNum = loadData.TotalCoinNum;
    }

    public void SaveGame()
    {
        Data data = new Data();

        //ハイスコア
        if (m_HighScore < m_Score)
        {
            data.HighScore = m_Score;
        }
        else
        {
            data.HighScore = m_OldHighScore;
        }
        //合計コイン数
        data.TotalCoinNum = m_TotalCoinNum + m_CoinNum;

        m_Json = JsonUtility.ToJson(data);
        FileIO.FileWrite("SaveData", m_Json);
    }

    public int score
    {
        get
        {
            return m_Score;
        }
        set
        {
            m_Score = value;
        }
    }

    public int highScore
    {
        get
        {
            return m_HighScore;
        }
    }

    public int coin
    {
        get
        {
            return m_CoinNum;
        }
        set
        {
            m_CoinNum = value;
        }
    }

    public int tmato
    {
        get
        {
            return m_TomatoNum;
        }
        set
        {
            m_TomatoNum = value;
        }
    }

    public int totalCoinNum
    {
        get
        {
            return m_TotalCoinNum;
        }
        set
        {
            m_TotalCoinNum = value;
        }
    }

    public bool isHighScore()
    {
        if (m_HighScore < m_Score)
            return true;
        return false;
    }

}
