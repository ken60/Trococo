using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Squid_ink;   //画面を覆うイカスミ
    [SerializeField]
    private Collider m_Collider;   //プレイヤーのCollider
    [SerializeField]
    private ParticleSystem m_DieParticle;   //プレイヤーのCollider
    [SerializeField]
    private Canvas m_Canvas;    //キャンバス
    [SerializeField]
    private float m_MoveSpeed;   //プレイヤーの移動速度
    [SerializeField]
    private float m_JumpForce;   //ジャンプ力
    [SerializeField]
    private float m_LateralJumpForce;    //横移動時のジャンプ力
    [SerializeField]
    private float m_CrouchTime; //しゃがむ時間
    [SerializeField]
    private float m_LerpRate;   //横移動の補間割合
    [SerializeField]
    private float m_LateralMotionSpeed;  //横移動のスピード
    [SerializeField]
    private float m_RayDist;    //地面判定のRayの長さ

    private Rigidbody m_Rigidbody;
    private Animator m_Animator;
    private Touch m_Touch;
    private Ray ray;
    private RaycastHit hit;
    private int m_CurrentRunningRail = 0;   //現在走っているレール
    private float m_TimeCount = 0;          //
    private bool m_isGrounded = false;
    private bool m_isJump = false;
    private bool m_isLateralMove = false;   //横移動中か
    private bool m_isCrouch = false;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
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
            m_Animator.SetTrigger("Jump");
        }
        else
        {
            m_isJump = false;
        }

        //下フリックでしゃがむ
        if (MobileInput.instance.IsFlickDown() && m_isGrounded)
        {
            m_isCrouch = true;
        }
        //しゃがみ中はプレイヤー自身の衝突判定を無効化
        if (m_isCrouch)
        {
            m_Collider.enabled = !m_isCrouch;
            m_TimeCount += Time.deltaTime;
            if (m_TimeCount >= m_CrouchTime)
            {
                m_TimeCount = 0.0f;
                m_isCrouch = false;
                m_Collider.enabled = !m_isCrouch;
            }
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
            //m_Rigidbody.AddForce(Vector3.up * m_JumpForce, ForceMode.VelocityChange);
        }

        if (m_isLateralMove)
        {
            //m_Rigidbody.AddForce(Vector3.up * (m_LateralJumpForce));
        }
    }

    //横移動
    private void LateralMotion()
    {
        //フリックで移動先のレールを変更
        if (MobileInput.instance.IsFlickRight())
        {
            m_CurrentRunningRail++;
        }
        if (MobileInput.instance.IsFlickLeft())
        {
            m_CurrentRunningRail--;
        }

        m_CurrentRunningRail = Mathf.Clamp(m_CurrentRunningRail, -1, 1);

        //レール間移動
        transform.position =
            Vector3.Lerp(transform.position,
            new Vector3(m_CurrentRunningRail, transform.position.y, transform.position.z), m_LerpRate);
    }

    void OnTriggerEnter(Collider hit)
    {
        //障害物
        if (hit.gameObject.tag == "Obstacle")
        {
            Instantiate(m_DieParticle, transform.position + new Vector3(0.0f, 0.8f, 0.0f), Quaternion.identity);
        }
        //障害物
        if (hit.gameObject.tag == "Squid")
        {
            GameObject obj = Instantiate(m_Squid_ink, transform.position + new Vector3(0.0f, 0.8f, 0.0f), Quaternion.identity) as GameObject;
            obj.transform.SetParent(m_Canvas.transform, false);
        }
    }
}
