using UnityEngine;
using NCMB;
using System.Collections.Generic;

public class NCMBManager : SingletonMonoBehaviour<NCMBManager>
{
    [SerializeField]
    int m_PassLength = 15;

    private string ps;
    private string STR_GROUP = "0123456789ABCDEFGHIJKLNMOPQRSTUVWXYZabcdefghijklnmopqrstuvwxyz";

    public void Login(string userName, string pass)
    {
        print("Login: " + userName + " " + pass);

        //NCMBUserのインスタンス作成 
        NCMBUser user = new NCMBUser();

        // ユーザー名とパスワードでログイン
        NCMBUser.LogInAsync(userName, pass, (NCMBException e) =>
         {
             if (e != null)
             {
                 Debug.Log("ログインに失敗: " + e.ErrorMessage);
             }
             else
             {
                 Debug.Log("ログインに成功！");
             }
         });
    }

    public void IsNameAvailable(string Name)
    {
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("Ranking");
        query.WhereEqualTo("Name", Name);
        query.CountAsync((int count, NCMBException e) =>
        {
            if (e == null)
            {
                if (count == 0)
                {
                    print("名前は登録されていない");
                }
                else
                {
                    print("すでに名前が登録されている");
                }
            }
        });
    }

    public void SendScore(int score)
    {
        // データストアのRankingクラスから、Nameをキーにして検索
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("Ranking");
        query.WhereEqualTo("Name", GameDataManager.Instance.userName);

        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            //検索成功したら    
            if (e == null)
            {
                if (objList.Count == 0)
                {
                    // ハイスコアが未登録の場合
                    NCMBObject obj = new NCMBObject("Ranking");
                    obj["Name"] = GameDataManager.Instance.userName;
                    obj["Score"] = score;
                    obj.SaveAsync();
                }
                else
                {
                    // ハイスコアが登録済みの場合
                    int cloudScore = System.Convert.ToInt32(objList[0]["Score"]); // クラウド上のスコアを取得

                    if (score > cloudScore)
                    {
                        // 今プレイしたスコアの方が高かったら、
                        objList[0]["Score"] = score;
                        objList[0].SaveAsync();
                    }
                }
            }
        });
    }
}
