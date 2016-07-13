using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;

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
    private Data m_Data = new Data();

    public void InitGame()
    {
        m_Score = 0;
        m_CoinNum = 0;
        m_TomatoNum = 0;
        m_isGameOver = false;
        m_isGamePlay = false;
        m_Data = new Data();
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
            return m_Data.TotalCoinNum;
        }
        set
        {
            TotalCoinNum = value;
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

    public bool IsHighScore()
    {
        if (m_Data.HighScore < m_Score)
            return true;
        return false;
    }

    public void LoadGame()
    {
        Data loadData = JsonMapper.ToObject<Data>(FileRead("SaveData"));
        m_Data.HighScore = loadData.HighScore;
        m_Data.TotalCoinNum = loadData.TotalCoinNum;

        Debug.Log(m_Data.HighScore);
        Debug.Log(m_Data.TotalCoinNum);
    }

    public void SaveGame()
    {
        //ハイスコア
        if (m_Data.HighScore < m_Score)
            m_Data.HighScore = m_Score;
        //合計コイン数
        m_Data.TotalCoinNum = m_Data.TotalCoinNum + m_CoinNum;

        m_Json = JsonMapper.ToJson(m_Data);
        FileWrite("SaveData", m_Json);
    }

    public void FileWrite(string name, string json)
    {
        // 保存するフォルダー
        string path = Application.persistentDataPath + "/Database/";

        // フォルダーがない場合は作成する
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        File.WriteAllText(path + name + ".json", json);
    }

    public string FileRead(string name)
    {
        string path = Application.persistentDataPath + "/Database/";

        //ファイルがない場合は作成
        if (!Directory.Exists(path))
        {
            FileWrite("SaveData", "");
        }
        return File.ReadAllText(path + name + ".json", System.Text.Encoding.UTF8);
    }

    public static void Delete(string path)
    {
        if (!Directory.Exists(path))
        {
            return;
        }

        //ディレクトリ以外の全ファイルを削除
        string[] filePaths = Directory.GetFiles(path);
        foreach (string filePath in filePaths)
        {
            File.SetAttributes(filePath, FileAttributes.Normal);
            File.Delete(filePath);
        }

        //ディレクトリの中のディレクトリも再帰的に削除
        string[] directoryPaths = Directory.GetDirectories(path);
        foreach (string directoryPath in directoryPaths)
        {
            Delete(directoryPath);
        }

        //中が空になったらディレクトリ自身も削除
        Directory.Delete(path, false);
    }
}
