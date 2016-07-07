using UnityEngine;
using System.Collections;
public class Particle : MonoBehaviour
{
    [SerializeField]
    private float m_lifeTime;

    void Start()
    {
        Destroy(gameObject, m_lifeTime);
    }
}