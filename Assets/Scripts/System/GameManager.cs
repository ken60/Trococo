using UnityEngine;
using System.Collections;
using System.IO;

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
        m_TomatoNum = 0;
    }

    public void LoadGame()
    {
        Data loadData = JsonUtility.FromJson(FileIO.FileRead("SaveData"), typeof(Data)) as Data;
        if (loadData == null) return;

        m_HighScore = loadData.HighScore;
        m_TotalCoinNum = loadData.TotalCoinNum;

        //Debug.Log(m_HighScore);
        //Debug.Log(m_TotalCoinNum);
    }

    public void SaveGame()
    {
        Data data = new Data();

        //ハイスコア
        if (m_HighScore < m_Score)
            data.HighScore = m_Score;
        //合計コイン数
        data.TotalCoinNum = m_TotalCoinNum + m_CoinNum;

        m_Json = JsonUtility.ToJson(data);
        FileIO.FileWrite("SaveData", m_Json);
    }

    public int Score
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

    public int Coin
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

    public int Tomato
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

    public int TotalCoinNum
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

    public bool IsHighScore()
    {
        if (m_HighScore < m_Score)
            return true;
        return false;
    }

}
