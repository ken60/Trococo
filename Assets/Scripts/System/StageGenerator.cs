using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageGenerator : MonoBehaviour
{
    public Transform m_Player;      //プレイヤーの座標
    public GameObject m_Rail;       //レール
    public GameObject m_Ground;     //地面
    public GameObject[] m_Plante;   //ステージオブジェクト
    public GameObject[] m_Obstacle; //障害物
    public int m_PreInstantiateRail = 20;       //最初に生成しておくレール数
    public int m_PreInstantiateGround = 3;      //最初に生成しておく地面数

    private List<GameObject> m_GeneratedRail = new List<GameObject>();
    private List<GameObject> m_GeneratedGround = new List<GameObject>();
    private List<GameObject> m_GeneratedPlante = new List<GameObject>();
    private List<GameObject> m_GeneratedObstacle = new List<GameObject>();
    private int m_RailPosZ = -2;        //レールを生成するZ座標
    private int m_GroundPosZ = -1;      //レールを生成するZ座標

    private void Start()
    {
        //初期配置レールを生成
        for (; m_RailPosZ < m_PreInstantiateRail; m_RailPosZ++)
        {
            GenObject(m_Rail, m_GeneratedRail, new Vector3(0.0f, 0.0f, m_RailPosZ));
        }

        //初期配置地面,植物等を生成
        for (; m_GroundPosZ < m_PreInstantiateGround; m_GroundPosZ++)
        {
            GenObject(m_Ground, m_GeneratedGround, new Vector3(0.0f, -1.0f, m_GroundPosZ * 10));
            GenObject(m_Plante[Random.Range(0, m_Plante.Length)], m_GeneratedPlante, new Vector3(0.0f, 0.0f, m_GroundPosZ * 10));
        }
    }

    private void Update()
    {
        if (m_Player.position.z > m_RailPosZ - (m_PreInstantiateRail - 1))
        {
            //画面外に出たレールを移動
            MoveObject(m_GeneratedRail, new Vector3(0.0f, 0.0f, m_RailPosZ));
            m_RailPosZ++;
        }

        if (m_Player.position.z * 0.1f > m_GroundPosZ - m_PreInstantiateGround)
        {
            //画面外に出た地面を移動
            MoveObject(m_GeneratedGround, new Vector3(0.0f, -1.0f, m_GroundPosZ * 10));

            //地面,ステージオブジェクトを生成
            GenObject(m_Plante[Random.Range(0, m_Plante.Length)], m_GeneratedPlante, new Vector3(0.0f, 0.0f, m_GroundPosZ * 10));

            //画面外に出たステージオブジェクトを削除
            RemoveObject(m_GeneratedPlante, 0);

            m_GroundPosZ++;
        }
    }


    private void GenObject(GameObject genObj, List<GameObject> list, Vector3 pos)
    {
        GameObject obj = Instantiate(genObj, pos, Quaternion.identity) as GameObject;
        list.Add(obj);
    }

    private void MoveObject(List<GameObject> list, Vector3 pos)
    {
        GameObject obj = list[0];
        list.RemoveAt(0);
        list.Add(obj);
        obj.transform.position = pos;
    }

    private void RemoveObject(List<GameObject> list, int index)
    {
        GameObject obj = list[index];
        list.RemoveAt(index);
        Destroy(obj.gameObject);
    }
}