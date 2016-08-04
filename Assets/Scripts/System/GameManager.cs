using UnityEngine;

[System.Serializable]
class Data
{
    public int highScore = 0;
    public int totalGoldCoinNum = 0;
    public int totalCopperCoinNum = 0;
}

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    private int m_Score = 0;
    private int m_GoldCoinNum = 0;
    private int m_CopperCoinNum = 0;
    private int m_HighScore = 0;
    private int m_OldHighScore = 0;
    private int m_TotalGoldCoinNum = 0;
    private int m_TotalCopperCoinNum = 0;
    private int m_TomatoNum = 0;
    private string m_Json;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void InitGame()
    {
        //print("InitGame");
        m_Score = 0;
        m_GoldCoinNum = 0;
        m_CopperCoinNum = 0;
        //m_HighScore = 0;
        //m_OldHighScore = 0;
        m_TomatoNum = 0;
    }

    public void LoadGame()
    {
        //print("LoadGame");
        Data loadData = JsonUtility.FromJson(FileIO.FileRead("SaveData"), typeof(Data)) as Data;
        if (loadData == null) return;

        m_HighScore = loadData.highScore;
        m_OldHighScore = loadData.highScore;
        m_TotalGoldCoinNum = loadData.totalGoldCoinNum;
        m_TotalCopperCoinNum = loadData.totalCopperCoinNum;
    }

    public void SaveGame()
    {
        //print("SaveGame");
        Data data = new Data();

        //ハイスコア
        if (m_HighScore < m_Score)
        {
            data.highScore = m_Score;
        }
        else
        {
            data.highScore = m_OldHighScore;
        }
        //合計コイン数
        data.totalGoldCoinNum = m_TotalGoldCoinNum + m_GoldCoinNum;
        data.totalCopperCoinNum = m_TotalCopperCoinNum + m_CopperCoinNum;

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

    public int goldCoin
    {
        get
        {
            return m_GoldCoinNum;
        }
        set
        {
            m_GoldCoinNum = value;
        }
    }

    public int copperCoin
    {
        get
        {
            return m_CopperCoinNum;
        }
        set
        {
            m_CopperCoinNum = value;
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

    public int totalGoldCoinNum
    {
        get
        {
            return m_TotalGoldCoinNum;
        }
    }

    public int totalCopperCoinNum
    {
        get
        {
            return m_TotalCopperCoinNum;
        }
    }

    public bool isHighScore()
    {
        if (m_HighScore < m_Score)
            return true;
        return false;
    }

}
