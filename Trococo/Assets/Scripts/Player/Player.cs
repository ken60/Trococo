using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Text m_Text;
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
        m_Text.text = "none";
    }

    void Update()
    {
        // Debug.DrawRay(transform.position, Vector3.down * m_RayDist, Color.red);

        if (Physics.Raycast(transform.position, Vector3.down, out hit, m_RayDist))
        {
            if (hit.collider.tag == "Ground")
                m_isGrounded = true;
        }

        //タップでジャンプ
        if (MobileInput.GetFlick() == MobileInput.TouchDir.UP && m_isGrounded)
        {
            m_Rigidbody.AddForce(Vector3.up * m_JumpForce);
            m_isGrounded = false;
        }

        //下方向フリックでしゃがむ
        if (MobileInput.GetFlick() == MobileInput.TouchDir.DOWN && m_isGrounded)
        {

        }
        

        switch (MobileInput.GetFlick())
        {
            case MobileInput.TouchDir.NONE:
                //m_Text.text = "なし";
                break;

            case MobileInput.TouchDir.UP:
                //上フリックされた時の処理
                m_Text.text = "上フリック";
                break;

            case MobileInput.TouchDir.DOWN:
                //下フリックされた時の処理
                m_Text.text = "下フリック";
                break;

            case MobileInput.TouchDir.RIGHT:
                //右フリックされた時の処理
                m_Text.text = "右フリック";
                break;

            case MobileInput.TouchDir.LEFT:
                //左フリックされた時の処理
                m_Text.text = "左フリック";
                break;

            case MobileInput.TouchDir.TOUCH:
                //タッチされた時の処理
                m_Text.text = "タッチ";
                break;
        }
    }
}
