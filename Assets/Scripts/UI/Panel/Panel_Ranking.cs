using NCMB;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Panel_Ranking : MonoBehaviour
{
    [SerializeField]
    private Text[] rankText;
    [SerializeField]
    private Text[] nameText;
    [SerializeField]
    private Text[] scoreText;

    private List<string> m_Rankers = null;
    private int currentRank = 0;
    private List<int> neighbors;
    private bool m_ShowRanks = false;


    private LeaderBoard lBoard;
    private HighScore currentHighScore;

    bool isScoreFetched;
    bool isRankFetched;
    bool isLeaderBoardFetched;

    void Start()
    {
        lBoard = new LeaderBoard();

        // フラグ初期化
        isScoreFetched = false;
        isRankFetched = false;
        isLeaderBoardFetched = false;

        // 現在のハイスコアを取得
        currentHighScore = new HighScore(-1, GameDataManager.Instance.userName);
        currentHighScore.Fetch();
    }

    void Update()
    {
        // 現在のハイスコアの取得が完了したら1度だけ実行
        if (currentHighScore.score != -1 && !isScoreFetched)
        {
            lBoard.FetchRank(currentHighScore.score);

            isScoreFetched = true;
        }

        // 現在の順位の取得が完了したら1度だけ実行
        if (lBoard.currentRank != 0 && !isRankFetched)
        {
            lBoard.FetchTopRankers();
            lBoard.FetchNeighbors();

            isRankFetched = true;
        }

        // ランキングの取得が完了したら1度だけ実行
        if (lBoard.neighbors != null && lBoard.topRankers != null && !isLeaderBoardFetched)
        {
            // 自分が1位のときと2位のときだけ順位表示を調整
            int offset = 3;
            if (lBoard.currentRank == 1) offset = 0;
            if (lBoard.currentRank == 2) offset = 1;
            if (lBoard.currentRank == 3) offset = 2;
            
            // 取得したトップ3ランキングを表示
            for (int i = 0; i < lBoard.topRankers.Count; ++i)
            {
                rankText[i].text = (i + 1).ToString() + "位";
                nameText[i].text = lBoard.topRankers[i].name;
                scoreText[i].text = lBoard.topRankers[i].score.ToString();

                //自分の名前を赤に
                if (lBoard.topRankers[i].name == GameDataManager.Instance.userName)
                {
                    nameText[i].color = Color.red;
                }
            }

            // 取得したライバルランキングを表示
            for (int i = 0; i < lBoard.neighbors.Count; ++i)
            {
                rankText[i + 3].text = (lBoard.currentRank - offset + i).ToString() + "位";
                nameText[i + 3].text = lBoard.neighbors[i].name;
                scoreText[i + 3].text = lBoard.neighbors[i].score.ToString();

                //自分の名前を赤に
                if (lBoard.neighbors[i].name == GameDataManager.Instance.userName)
                {
                    nameText[i + 3].color = Color.red;
                }
            }

            isLeaderBoardFetched = true;
        }
    }
}
