using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float m_JumpForce;

    public  bool m_isGrounded;

    private Rigidbody m_Rigidbody;
    private Touch m_Touch;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_isGrounded = false;

    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space) && m_isGrounded)
        {
            m_Rigidbody.AddForce(Vector3.up * m_JumpForce);
            m_isGrounded = false;
        }


#else
        if (Input.touchCount > 0 && m_isGrounded)
        {
            m_Rigidbody.AddForce(Vector3.up * m_JumpForce);
        m_isGrounded = false;
        }
#endif
    }

    void OnCollisionEnter(Collision col)
    {
        m_isGrounded = true;

    }
}
