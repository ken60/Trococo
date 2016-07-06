using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float m_MoveSpeed = 2.0f;
    public float m_JumpForce;
    public float m_LateralForce;
    public float m_RayDist = 0.5f;
    public float m_LateralMotionSpeed;
    public bool m_isGrounded = false;

    private Rigidbody m_Rigidbody;
    private Touch m_Touch;
    private Ray ray;
    private RaycastHit hit;
    private int m_CurrentRunningRail = 0; //現在走っているレール
    private bool m_isJump = false;
    private bool m_isLateralMove = false;

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
        Debug.DrawRay(RayPos, Vector3.down * m_RayDist, Color.red);
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

        //地面についている時のみ横移動
        if (m_isGrounded)
        {
            LateralMotion();
        }
    }

    private void FixedUpdate()
    {
        if (m_isJump)
        {
            m_Rigidbody.AddForce(Vector3.up * m_JumpForce);
        }
        if (m_isLateralMove)
        {
            m_Rigidbody.AddForce(Vector3.up * (m_JumpForce * 0.5f));
        }
    }

    //横移動
    private void LateralMotion()
    {
        if (MobileInput.instance.IsFlickRight())
        {
            m_CurrentRunningRail++;
        }
        if (MobileInput.instance.IsFlickLeft())
        {
            m_CurrentRunningRail--;
        }
        m_CurrentRunningRail = Mathf.Clamp(m_CurrentRunningRail, -1, 1);

        transform.position =
            Vector3.Lerp(transform.position,
            new Vector3(m_CurrentRunningRail, transform.position.y, transform.position.z), 0.2f);
    }

    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Obstacle")
        {
            print("HIT!!");
        }
    }
}
