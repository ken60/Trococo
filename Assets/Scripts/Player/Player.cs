using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float m_MoveSpeed = 2.0f;
    public float m_JumpForce;
    public float m_RayDist = 0.5f;
    public bool m_isGrounded = false;

    private Rigidbody m_Rigidbody;
    private Touch m_Touch;
    private Ray ray;
    private RaycastHit hit;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //プレイヤーの移動
        transform.position += new Vector3(0.0f, 0.0f, m_MoveSpeed) * Time.deltaTime;


        //接地判定
        Vector3 RayPos = transform.position + new Vector3(0.0f, 0.5f, 0.0f);
        //Debug.DrawRay(RayPos, Vector3.down * m_RayDist, Color.red);
        if (Physics.Raycast(RayPos, Vector3.down, out hit, m_RayDist))
        {
            if (hit.collider.tag == "Rail")
            {
                m_isGrounded = true;
            }
        }

        //タップでジャンプ
        if (MobileInput.instance.IsFlickUp() && m_isGrounded)
        {
            m_Rigidbody.AddForce(Vector3.up * m_JumpForce);
            m_isGrounded = false;
        }

        //GetTouchType
        if (MobileInput.instance.IsFlickDown() && m_isGrounded)
        {
            print("しゃがむ");
        }
    }
}
