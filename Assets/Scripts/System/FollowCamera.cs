using UnityEngine;
using System.Collections;
public class FollowCamera : MonoBehaviour
{
    public float m_LerpRate;
    public Vector3 m_Offset;

    private Transform m_Player;

    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    void Update()
    {
        transform.position = new Vector3(m_Player.position.x, 0.0f, m_Player.position.z) + m_Offset;
    }
}