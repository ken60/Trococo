using UnityEngine;
using System.Collections;
public class Coin : MonoBehaviour
{
    [SerializeField]
    private float m_RotateSpeed;
    [SerializeField]
    private float m_UpSpeed;

    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(new Vector3(0.0f, m_RotateSpeed * Time.deltaTime, 0.0f));
    }

    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Player")
        {

        }
    }

    void GetCoin()
    {
        //transform.position += new Vector3(0.0f, m_UpSpeed, 0.0f);
    }
}