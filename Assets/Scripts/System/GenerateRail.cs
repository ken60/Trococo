using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateRail : MonoBehaviour
{
    public GameObject m_Rail;
    public float m_MoveSpeed = 1.0f;

    private List<GameObject> m_GeneratedRail = new List<GameObject>();
    private int m_PosZ = 0;
    private float time = 0.0f;

    void Start()
    {
        //初期配置レールを生成
        for (m_PosZ = 0; m_PosZ < 15; m_PosZ++)
        {
            GameObject rail = Instantiate(m_Rail, transform.position + new Vector3(0, 0, m_PosZ), Quaternion.identity) as GameObject;
            rail.transform.SetParent(transform);
            m_GeneratedRail.Add(rail);
        }
    }

    void Update()
    {
        //レールの移動
        transform.position += new Vector3(0.0f, 0.0f, -m_MoveSpeed) * Time.deltaTime;

        time += Time.deltaTime;
        if (time >= 1.0f / m_MoveSpeed)
        {
            time = 0.0f;
            GameObject rail = Instantiate(m_Rail, transform.position + new Vector3(0, 0, m_PosZ), Quaternion.identity) as GameObject;
            rail.transform.SetParent(transform);
            m_GeneratedRail.Add(rail);
            m_PosZ++;
            

            //画面から消えたレールを削除
            rail = m_GeneratedRail[0];
            m_GeneratedRail.RemoveAt(0);
            Destroy(rail.gameObject);
        }
    }
}