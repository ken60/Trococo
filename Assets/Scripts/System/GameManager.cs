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
    private int m_HighScore = 0;
    private int m_CoinNum = 0;
    private int m_TotalCoinNum = 0;
    private int m_TomatoNum = 0;
    private bool m_isGamePlay = false;
    private bool m_isGameOver = false;
    private string m_Json;
    Data m_Data = new Data();

    public void InitGame()
    {
        m_Score = 0;
        m_CoinNum = 0;
        m_TomatoNum = 0;
        m_isGameOver = false;
        m_isGamePlay = false;
    }

    public void LoadGame()
    {/*
        Data data = new Data(); //初期化が必要
        JsonUtility.FromJsonOverwrite(m_Json, data);
        m_HighScore = data.HighScore;
        m_TotalCoinNum = data.TotalCoinNum;
        print(data.HighScore + " " + data.TotalCoinNum);
        */
        string loadData = null;
        loadData = FileRead("SaveData");
        Debug.Log(loadData);
    }

    public void SaveGame()
    {
        m_Data.HighScore = m_HighScore + m_Score;
        m_Data.TotalCoinNum = m_TotalCoinNum + m_CoinNum;
        m_Json = JsonUtility.ToJson(m_Data);
        Debug.Log(m_Json);
        FileWrite("SaveData", m_Json);
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

    public bool IsGameOver
    {
        get
        {
            return m_isGameOver;
        }
        set
        {
            m_isGameOver = value;
        }
    }

    public bool IsGamePlay
    {
        get
        {
            return m_isGamePlay;
        }
        set
        {
            m_isGamePlay = value;
        }
    }

    public void FileWrite(string name, string json)
    {
        print("Called");
        // 保存するフォルダー
        string path = Application.persistentDataPath + "/Database/";
        print(Application.persistentDataPath + "/Database/");

        // フォルダーがない場合は作成する
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
            print("Create");
        }

        File.WriteAllText(path + name + ".json", json);
    }

    public string FileRead(string name)
    {
        string path = Application.persistentDataPath + "/Database/";
        return File.ReadAllText(path + name + ".json", System.Text.Encoding.UTF8);
    }
}