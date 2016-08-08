using UnityEngine;

[System.Serializable]
class Data
{
    public int highScore = 0;           //ハイスコア
    public int totalGoldCoinNum = 0;    //累計金コイン数  
    public int playCharID = 0;          //選択しているキャラクターID
    public bool[] isCharAvailable = new bool[6];          //キャラを開放しているか
}

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField]
    private bool[] m_isCharAvailable = new bool[] { false };       //キャラを開放しているか

    private int m_Score = 0;                //セーブするスコア
    private int m_GoldCoinNum = 0;          //金コイン数
    private int m_HighScore = 0;            //ハイスコア
    private int m_OldHighScore = 0;         //ハイスコア(ハイスコアの判定用)
    private int m_TotalGoldCoinNum = 0;     //累計金コイン数
    private int m_PlayerCharID = 0;         //選択しているキャラID
    private string m_Json;

    void Start()
    {
        m_isCharAvailable[0] = true;
    }

    public void InitGame()
    {
        m_Score = 0;
        m_GoldCoinNum = 0;
    }

    public void LoadGame()
    {
        Data loadData = JsonUtility.FromJson(FileIO.FileRead("SaveData"), typeof(Data)) as Data;
        if (loadData == null) return;

        m_HighScore = loadData.highScore;
        m_OldHighScore = loadData.highScore;
        m_TotalGoldCoinNum = loadData.totalGoldCoinNum;

        for (int i = 0; i < m_isCharAvailable.Length; i++)
        {
            m_isCharAvailable[i] = loadData.isCharAvailable[i];
        }
    }

    public void SaveGame()
    {
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

        for (int i = 0; i < m_isCharAvailable.Length; i++)
        {
            data.isCharAvailable[i] = m_isCharAvailable[i];
        }

        m_Json = JsonUtility.ToJson(data);
        FileIO.FileWrite("SaveData", m_Json);
    }


    //ゲーム中のスコア
    public int score
    {
        get
        { return m_Score; }
        set { m_Score = value; }
    }

    //ハイスコアを取得
    public int highScore
    {
        get { return m_HighScore; }
    }

    //ゲーム中取得した金コイン数
    public int goldCoin
    {
        get { return m_GoldCoinNum; }
        set { m_GoldCoinNum = value; }
    }

    //累計金コイン数
    public int totalGoldCoinNum
    {
        get { return m_TotalGoldCoinNum; }
        set { m_TotalGoldCoinNum = value; }
    }

    //ハイスコアかどうか
    public bool IsHighScore()
    {
        if (m_HighScore < m_Score)
            return true;
        return false;
    }

    //選択しているキャラID
    public int playCharID
    {
        get { return m_PlayerCharID; }
        set { m_PlayerCharID = value; }
    }

    //全キャラクター数
    public int totalCharacterNumber
    {
        get { return m_isCharAvailable.Length; }
    }

    //指定のキャラを開放しているか
    public bool IsCharAvailable(int id)
    {
        if (id > m_isCharAvailable.Length - 1)
            id = m_isCharAvailable.Length - 1;

        if (m_isCharAvailable[id] == true)
            return true;

        return false;
    }

    //キャラを開放
    public void OpenCharacter(int id)
    {
        if (id > m_isCharAvailable.Length - 1)
            id = m_isCharAvailable.Length - 1;

        if (m_isCharAvailable[id] == true) return;

        m_isCharAvailable[id] = true;
    }
}
