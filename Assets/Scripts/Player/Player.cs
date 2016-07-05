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
    private bool m_isJump;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
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

        //上フリックでジャンプ
        if (MobileInput.instance.IsFlickUp() && m_isGrounded)
        {
            m_isJump = true;
            m_isGrounded = false;
        }
        else
        {
            m_isJump = false;
        }

        //下フリックでしゃがむ
        if (MobileInput.instance.IsFlickDown() && m_isGrounded)
        {
            print("しゃがむ");
        }
    }

    private void FixedUpdate()
    {
        if (m_isJump)
        {
            m_Rigidbody.AddForce(Vector3.up * m_JumpForce);
        }
    }

    void OnTriggerEnter(Collider hit)
    {
        if(hit.gameObject.tag == "Obstacle")
        {
            print("HIT!!");
        }
    }
}
