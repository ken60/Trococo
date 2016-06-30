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
        if (MobileInput.GetTouch(MobileInput.TouchType.UP) && m_isGrounded)
        {
            m_Rigidbody.AddForce(Vector3.up * m_JumpForce);
            m_isGrounded = false;
        }

        //GetTouchType
        if (MobileInput.GetTouch(MobileInput.TouchType.DOWN) && m_isGrounded)
        {
            print("しゃがむ");
        }
        

        switch (MobileInput.GetFlick())
        {
            case MobileInput.TouchType.NONE:
                //m_Text.text = "なし";
                break;

            case MobileInput.TouchType.UP:
                //上フリックされた時の処理
                m_Text.text = "上フリック";
                break;

            case MobileInput.TouchType.DOWN:
                //下フリックされた時の処理
                m_Text.text = "下フリック";
                break;

            case MobileInput.TouchType.RIGHT:
                //右フリックされた時の処理
                m_Text.text = "右フリック";
                break;

            case MobileInput.TouchType.LEFT:
                //左フリックされた時の処理
                m_Text.text = "左フリック";
                break;

            case MobileInput.TouchType.TOUCH:
                //タッチされた時の処理
                m_Text.text = "タッチ";
                break;
        }
    }
}
