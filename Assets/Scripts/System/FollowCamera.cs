using UnityEngine;
using System.Collections;
public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    private float m_LerpRate;
    [SerializeField]
    private Vector3 m_Offset;

    private Transform m_Player;

    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            new Vector3(m_Player.position.x, 0.0f, m_Player.position.z) + m_Offset,
            m_LerpRate);
    }
}