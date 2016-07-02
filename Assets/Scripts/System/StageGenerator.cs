using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageGenerator : MonoBehaviour
{
    public GameObject m_Rail;
    public GameObject m_Ground;
    public GameObject[] m_Rock;
    public Transform m_Player;  //プレイヤーの座標
    public int m_PreInstantiateRail = 20; //生成しておくレール数
    public int m_PreInstantiateGround = 3; //生成しておく地面数

    private List<GameObject> m_GeneratedRail = new List<GameObject>();
    private List<GameObject> m_GeneratedGround = new List<GameObject>();
    private int m_RailPosZ = -2;    //レールを生成するZ座標
    private int m_GroundPosZ = -1;    //レールを生成するZ座標

    private void Start()
    {
        //初期配置レールを生成
        for (; m_RailPosZ < m_PreInstantiateRail; m_RailPosZ++)
        {
            ObjectGen(m_Rail, m_GeneratedRail, new Vector3(0.0f, 0.0f, m_RailPosZ));
        }
        //初期配置地面を生成
        for (; m_GroundPosZ < m_PreInstantiateGround; m_GroundPosZ++)
        {
            ObjectGen(m_Ground, m_GeneratedGround, new Vector3(0.0f, -1.0f, m_GroundPosZ * 10));
        }
    }

    private void Update()
    {
        //画面外に出たレールを移動
        if (m_Player.position.z > m_RailPosZ - (m_PreInstantiateRail - 1))
        {
            ObjectMove(m_GeneratedRail, new Vector3(0.0f, 0.0f, m_RailPosZ));
            m_RailPosZ++;
        }

        //画面外に出た地面を移動
        if (m_Player.position.z * 0.1f > m_GroundPosZ - m_PreInstantiateGround)
        {
            ObjectMove(m_GeneratedGround, new Vector3(0.0f, -1.0f, m_GroundPosZ * 10));
            m_GroundPosZ++;
        }
    }

    private void ObjectGen(GameObject genObj, List<GameObject> list, Vector3 pos)
    {
        GameObject obj = Instantiate(genObj, pos, Quaternion.identity) as GameObject;
        list.Add(obj);
    }

    private void ObjectMove(List<GameObject> list, Vector3 pos)
    {
        GameObject obj = list[0];
        list.RemoveAt(0);
        list.Add(obj);
        obj.transform.position = pos;
    }
}