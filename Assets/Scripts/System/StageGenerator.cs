using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageGenerator : MonoBehaviour
{
    [SerializeField]
    private Transform m_Player;      //プレイヤーの座標
    [SerializeField]
    private GameObject m_Rail;       //レール
    [SerializeField]
    private GameObject m_Ground;     //地面
    [SerializeField]
    private GameObject[] m_Plante;   //ステージオブジェクト
    [SerializeField]
    private GameObject[] m_Obstacle; //障害物
    [SerializeField]
    private int m_PreInstantiateRail = 20;      //予め生成しておくレール数
    [SerializeField]
    private int m_PreInstantiateGround = 3;     //予め生成しておく地面数
    [SerializeField]
    private int m_PreInstantiateObstacle = 3;   //予め生成しておく障害物数

    private List<GameObject> m_GeneratedRail = new List<GameObject>();
    private List<GameObject> m_GeneratedGround = new List<GameObject>();
    private List<GameObject> m_GeneratedPlante = new List<GameObject>();
    private List<GameObject> m_GeneratedObstacle = new List<GameObject>();
    private int m_RailPosZ = -2;        //レールを生成するZ座標
    private int m_GroundPosZ = -1;      //地面を生成するZ座標
    private int m_ObstaclePosZ = 1;    //障害物を生成するZ座標
    private int m_GroundSizeZ = 10;     //地面のZサイズ

    private void Start()
    {
        //初期配置レールを生成
        for (; m_RailPosZ < m_PreInstantiateRail; m_RailPosZ++)
        {
            GenObject(m_Rail, m_GeneratedRail, new Vector3(1.0f, 0.0f, m_RailPosZ));    //右
            GenObject(m_Rail, m_GeneratedRail, new Vector3(0.0f, 0.0f, m_RailPosZ));    //中
            GenObject(m_Rail, m_GeneratedRail, new Vector3(-1.0f, 0.0f, m_RailPosZ));   //左

        }

        //初期配置地面を生成
        for (; m_GroundPosZ < m_PreInstantiateGround; m_GroundPosZ++)
        {
            GenObject(m_Ground, m_GeneratedGround, new Vector3(0.0f, -1.0f, m_GroundPosZ * m_GroundSizeZ));
        }

        //初期配置障害物を生成
        for (; m_ObstaclePosZ < m_PreInstantiateGround; m_ObstaclePosZ++)
        {
            GenObject(m_Obstacle[Random.Range(0, m_Obstacle.Length)], m_GeneratedObstacle, new Vector3(0.0f, 0.0f, m_ObstaclePosZ * 10));
        }
    }

    private void Update()
    {
        //プレイヤーが、予め生成しておくレール数より進んだら
        if (m_Player.position.z > m_RailPosZ - (m_PreInstantiateRail - 2))
        {
            //画面外に出たレールを移動
            MoveObject(m_GeneratedRail, new Vector3(1.0f, 0.0f, m_RailPosZ));   //右
            MoveObject(m_GeneratedRail, new Vector3(0.0f, 0.0f, m_RailPosZ));   //中
            MoveObject(m_GeneratedRail, new Vector3(-1.0f, 0.0f, m_RailPosZ));  //左
            m_RailPosZ++;
        }

        if (m_Player.position.z > (m_GroundPosZ - m_PreInstantiateGround) * m_GroundSizeZ)
        {
            //画面外に出た地面を移動
            MoveObject(m_GeneratedGround, new Vector3(0.0f, -1.0f, m_GroundPosZ * m_GroundSizeZ));

            //障害物を生成
            GenObject(m_Obstacle[Random.Range(0, m_Obstacle.Length)], m_GeneratedObstacle, new Vector3(0.0f, 0.0f, m_ObstaclePosZ * 10));

            RemoveObject(m_GeneratedObstacle, 0);

            m_GroundPosZ++;
            m_ObstaclePosZ++;
        }


        //画面外に出た障害物を削除
        if (m_Player.position.z > m_ObstaclePosZ * 10 - (m_PreInstantiateObstacle - 1))
        {
            //print("RemoveObject");
            //RemoveObject(m_GeneratedObstacle, 0);
        }
    }

    //オブジェクトを生成
    private void GenObject(GameObject genObj, List<GameObject> list, Vector3 pos)
    {
        GameObject obj = Instantiate(genObj, pos, Quaternion.identity) as GameObject;
        list.Add(obj);
    }

    //オブジェクトを移動
    private void MoveObject(List<GameObject> list, Vector3 pos)
    {
        GameObject obj = list[0];
        list.RemoveAt(0);
        list.Add(obj);
        obj.transform.position = pos;
    }

    //オブジェクトを削除
    private void RemoveObject(List<GameObject> list, int index)
    {
        GameObject obj = list[index];
        list.RemoveAt(index);
        Destroy(obj.gameObject);
    }
}