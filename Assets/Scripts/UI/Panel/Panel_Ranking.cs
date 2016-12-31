using UnityEngine;
using UnityEngine.UI;
using NCMB;
using System.Collections.Generic;

public class Panel_Ranking : MonoBehaviour
{
    [SerializeField]
    private Text[] rankText;
    [SerializeField]
    private Text[] nameScore;

    private List<string> m_Rankers = null;
    private bool m_ShowRanks = false;

    void Start()
    {
    }

    void Update()
    {
        if (!m_ShowRanks)
        {
            //ShowRanking();
            FetchTopRankers();
            m_ShowRanks = true;
        }
    }

    void ShowRanking()
    {
        // データストアのRankingクラスから、Nameをキーにして検索
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("Ranking");
        query.WhereEqualTo("Name", GameDataManager.Instance.userName);

        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            //検索成功したら    
            if (e == null)
            {
                rankText[0].text = "1位";
                nameScore[0].text = System.Convert.ToString(objList[0]["Name"]) + "　" + System.Convert.ToInt32(objList[0]["Score"]);

                if (objList.Count == 0)
                {
                    // ハイスコアが未登録の場合

                }
                else
                {
                    // ハイスコアが登録済みの場合
                    int cloudScore = System.Convert.ToInt32(objList[0]["Score"]); // クラウド上のスコアを取得

                }
            }
        });
    }

    public void FetchTopRankers()
    {
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("Ranking");

        // Scoreを降順でデータを取得するように設定
        query.OrderByDescending("Score");

        // 検索件数を設定
        query.Limit = 3;

        // データストアを検索
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                //検索に失敗した場合の処理
                Debug.Log("検索に失敗しました " + e.ErrorCode);
            }
            else
            {
                List<string> list = new List<string>();

                foreach (NCMBObject obj in objList)
                {
                    string name;
                    name = System.Convert.ToString(obj["Name"]) + "　" + System.Convert.ToInt32(obj["Score"]).ToString();

                    list.Add(name);
                }

                for(int i=0;i<list.Count;i++)
                {
                    nameScore[i].text = list[i].ToString();
                }
            }
        });
    }
}
