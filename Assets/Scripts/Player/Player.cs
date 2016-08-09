using UnityEngine;
using TouchScript.Gestures;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_Squid_ink;   //画面を覆うイカスミ
    [SerializeField]
    private GameObject m_TouchScriptObj;
    [SerializeField]
    private Collider m_Collider;   //プレイヤーのCollider
    [SerializeField]
    private ParticleSystem m_DieParticle;   //プレイヤーのCollider
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
    [SerializeField]
    private float MinDistance = 1.0f;   //フリックの最小距離
    [SerializeField]
    private float FlickTime = 0.3f;     //フリックが有効な時間

    private GameObject m_Canvas;
    private Rigidbody m_Rigidbody;
    private Animator m_Animator;
    private Touch m_Touch;
    private Ray ray;
    private RaycastHit hit;
    private int m_CurrentRunningRail = 0;   //現在走っているレール
    private float m_TimeCount = 0;
    private bool m_isGrounded = false;
    private bool m_isJump = false;
    private bool m_isCrouch = false;

    void Start()
    {
        m_Canvas = GameObject.Find("Canvas");
        m_Rigidbody = GetComponent<Rigidbody>();
        //m_Animator = GetComponent<Animator>();

        m_TouchScriptObj.GetComponent<TapGesture>().Tapped += HandleTapped;
        m_TouchScriptObj.GetComponent<FlickGesture>().StateChanged += HandleFlick;
        m_TouchScriptObj.GetComponent<FlickGesture>().MinDistance = 1f;
        m_TouchScriptObj.GetComponent<FlickGesture>().FlickTime = 0.3f;
    }

    public void InitPlayer()
    {
        transform.position = Vector3.zero;
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        m_CurrentRunningRail = 0;
        m_TimeCount = 0;

    }

    //タップ時に呼ばれる
    void HandleTapped(object sender, System.EventArgs e)
    {
        if (GameSceneManager.Instance.isGameOver ||
            !GameSceneManager.Instance.isGamePlaying) return;

        if (m_isGrounded)
        {
            m_isJump = true;
            //m_Animator.SetTrigger("Jump");
        }
    }

    //フリック時に呼ばれる
    void HandleFlick(object sender, System.EventArgs e)
    {
        if (GameSceneManager.Instance.isGameOver ||
            !GameSceneManager.Instance.isGamePlaying) return;

        var gesture = sender as FlickGesture;

        if (gesture.State != FlickGesture.GestureState.Recognized)
            return;

        //Left
        if (gesture.ScreenFlickVector.x < 0)
        {
            m_CurrentRunningRail--;
        }
        //Right
        else if (gesture.ScreenFlickVector.x > 0)
        {
            m_CurrentRunningRail++;
        }
        //Down
        else if (gesture.ScreenFlickVector.y < 0)
        {
            m_isCrouch = true;
        }
        //Up
        else if (gesture.ScreenFlickVector.y > 0)
        {

        }
    }

    void Update()
    {
        if (GameSceneManager.Instance.isGameOver ||
            !GameSceneManager.Instance.isGamePlaying) return;

        //プレイヤーの移動
        transform.position += new Vector3(0.0f, 0.0f, m_MoveSpeed) * Time.deltaTime;
        //m_Rigidbody.velocity = (Vector3.forward * m_MoveSpeed) *Time.deltaTime;

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

        //地面についている時
        if (m_isGrounded)
        {
            //横移動
            LateralMotion();            
        }
        
        //スコア
        GameManager.Instance.score = (int)transform.position.z;
    }

    void FixedUpdate()
    {

        if (m_isJump)
        {
            m_isJump = false;
            m_isGrounded = false;
            m_Rigidbody.AddForce(Vector3.up * m_JumpForce);
        }
    }


    //横移動
    void LateralMotion()
    {
        m_CurrentRunningRail = Mathf.Clamp(m_CurrentRunningRail, -1, 1);

        //レール間移動
        transform.position =
            Vector3.Lerp(transform.position,
            new Vector3(m_CurrentRunningRail, transform.position.y, transform.position.z), m_LerpRate);
    }

    void OnTriggerEnter(Collider hit)
    {
        if (GameSceneManager.Instance.colliderEnabled)
        {
            //障害物
            if (hit.gameObject.tag == "Obstacle")
            {
                GameSceneManager.Instance.isGameOver = true;
                Instantiate(m_DieParticle, transform.position, m_DieParticle.transform.rotation);
            }
            //イカ
            if (hit.gameObject.tag == "Squid")
            {
                GameObject obj = Instantiate(m_Squid_ink[Random.Range(0, m_Squid_ink.Length)], transform.position + new Vector3(0.0f, 0.8f, 0.0f), Quaternion.identity) as GameObject;
                obj.transform.SetParent(m_Canvas.transform, false);
            }

            //コイン
            if (hit.gameObject.tag == "Gold_Coin")
            {
                Destroy(hit.gameObject);
                GameManager.Instance.goldCoin += 1;
            }

            //トマト
            if (hit.gameObject.tag == "Tomato")
            {
                Destroy(hit.gameObject);
            }
        }
    }
}
