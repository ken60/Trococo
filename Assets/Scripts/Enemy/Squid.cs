using UnityEngine;

public class Squid : MonoBehaviour
{
    [SerializeField]
    private float m_Speed;
    [SerializeField]
    private float m_RotateSpeed;

    private float m_Phase;
    private float m_Sin;

    void Start()
    {

    }

    void Update()
    {
        m_Phase += 2;
        m_Sin = Mathf.Sin(m_Phase * Mathf.PI / 180) * Time.deltaTime / m_Speed;

        //Movement
        transform.position = new Vector3(transform.position.x, transform.position.y + m_Sin, transform.position.z);
        transform.Rotate(new Vector3(0.0f, m_RotateSpeed, 0.0f));
    }
}