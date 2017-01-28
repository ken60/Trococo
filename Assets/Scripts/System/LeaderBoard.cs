using NCMB;
using UnityEngine;
using System.Collections.Generic;

public class HighScore
{
    public int score { get; set; }
    public string name { get; private set; }

    public HighScore(int _score, string _name)
    {
        score = _score;
        name = _name;
    }

    public void Fetch()
    {
        // データストアの「HighScore」クラスから、Nameをキーにして検索
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("Ranking");
        query.WhereEqualTo("Name", name);

        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            //検索成功したら
            if (e == null)
            {
                // ハイスコアが未登録だったら
                if (objList.Count == 0)
                {
                    NCMBObject obj = new NCMBObject("Ranking");
                    obj["Name"] = name;
                    obj["Score"] = 0;
                    obj.SaveAsync();
                    score = 0;
                }
                // ハイスコアが登録済みだったら
                else
                {
                    score = System.Convert.ToInt32(objList[0]["Score"]);
                }
            }
        });
    }
}

public class LeaderBoard
{
    public int currentRank = 0;
    public List<HighScore> topRankers = null;
    public List<HighScore> neighbors = null;
    
    public void FetchRank(int currentScore)
    {
        // データスコアの「HighScore」から検索
        NCMBQuery<NCMBObject> rankQuery = new NCMBQuery<NCMBObject>("Ranking");
        rankQuery.WhereGreaterThan("Score", currentScore);

        rankQuery.CountAsync((int count, NCMBException e) =>
        {
            if (e != null)
            {
                //件数取得失敗
            }
            else
            {
                //件数取得成功
                currentRank = count + 1; // 自分よりスコアが上の人がn人いたら自分はn+1位
            }
        });
    }

    public void FetchTopRankers()
    {
        // データストアの「HighScore」クラスから検索
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("Ranking");
        query.OrderByDescending("Score");
        query.Limit = 3;

        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                //検索失敗時の処理
            }
            else
            {
                List<HighScore> list = new List<HighScore>();

                foreach (NCMBObject obj in objList)
                {
                    string n = System.Convert.ToString(obj["Name"]);
                    int s = System.Convert.ToInt32(obj["Score"]);

                    list.Add(new HighScore(s, n));
                }
                topRankers = list;
            }
        });
    }
    
    public void FetchNeighbors()
    {
        // スキップする数を決める（ただし自分が1位か2位のときは調整する）
        int numSkip = currentRank - 4;
        if (numSkip < 0) numSkip = 0;

        // データストアの「HighScore」クラスから検索
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("Ranking");
        query.OrderByDescending("Score");
        query.Skip = numSkip;
        query.Limit = 7;

        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                //検索失敗時の処理
            }
            else
            {
                List<HighScore> list = new List<HighScore>();

                foreach (NCMBObject obj in objList)
                {
                    string n = System.Convert.ToString(obj["Name"]);
                    int s = System.Convert.ToInt32(obj["Score"]);

                    list.Add(new HighScore(s, n));
                }
                neighbors = list;
            }
        });
    }
}