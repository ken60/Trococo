using UnityEngine;
using TouchScript.Gestures;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_Squid_ink;   //画面を覆うイカスミ

    [SerializeField]
    private GameObject[] m_AllCharacters;   //すべてのキャラクター

    [SerializeField]
    private Transform m_CharGenPosition; //キャラクターの生成位置

    [SerializeField]
    private GameObject m_TouchScriptObj;    //タッチ関連

    [SerializeField]
    private ParticleSystem m_DieParticle;   //ゲームオーバーのパーティクル

    [SerializeField]
    private GameObject[] m_ChangeParticle;    //キャラチェンジ時のパーティクル

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
    private float m_Angle = 30.0f;

    private GameObject m_Canvas;
    private GameObject m_Character; //生成されたキャラクター
    private Vector3 m_CharScale = new Vector3(1.0f, 1.0f, 1.0f);
    private Rigidbody m_Rigidbody;
    private Collider m_Collider;   //プレイヤーのCollider
    private Animator m_Animator;
    private Touch m_Touch;
    private Ray ray;
    private RaycastHit hit;
    private int m_CurrentRunningRail = 0;   //現在走っているレール
    private int m_PreCharID = 0;    //前の選択キャラID
    private float m_TimeCount = 0;
    private bool m_isGrounded = false;
    private bool m_isJump = false;
    private bool m_isCrouch = false;

    void Start()
    {
        m_Canvas = GameObject.Find("Canvas");
        m_Rigidbody = GetComponent<Rigidbody>();
        //m_Animator = GetComponent<Animator>();

        FlickGesture flick = m_TouchScriptObj.GetComponent<FlickGesture>();
        m_TouchScriptObj.GetComponent<TapGesture>().Tapped += HandleTapped;
        flick.StateChanged += HandleFlick;
        flick.MinDistance = 0.3f;
        flick.FlickTime = 0.25f;
        m_PreCharID = GameDataManager.Instance.playCharID;

        //初期キャラクター生成
        m_Character = Instantiate(m_AllCharacters[m_PreCharID], m_CharGenPosition.position, m_CharGenPosition.rotation) as GameObject;
        m_Character.transform.SetParent(transform);
        m_Collider = m_Character.GetComponent<BoxCollider>();
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
            GameSceneManager.Instance.isPause ||
            !GameSceneManager.Instance.isGamePlaying)
            return;
               
    }

    //フリック時に呼ばれる
    void HandleFlick(object sender, System.EventArgs e)
    {
        if (GameSceneManager.Instance.isGameOver ||
            GameSceneManager.Instance.isPause ||
            !GameSceneManager.Instance.isGamePlaying ||
            !m_isGrounded)
            return;

        var gesture = sender as FlickGesture;

        if (gesture.State != FlickGesture.GestureState.Recognized)
            return;

        //フリック方向の判定
        if (Mathf.Abs(gesture.ScreenFlickVector.y) < Mathf.Abs(gesture.ScreenFlickVector.x))
        {
            //Right
            if (m_Angle < gesture.ScreenFlickVector.x)
            {
                m_CurrentRunningRail++;
            }
            //Left
            else if (gesture.ScreenFlickVector.x < -m_Angle)
            {
                m_CurrentRunningRail--;
            }
        }
        else if (Mathf.Abs(gesture.ScreenFlickVector.x) < Mathf.Abs(gesture.ScreenFlickVector.y))
        {
            //Up
            if (m_Angle < gesture.ScreenFlickVector.y)
            {
                if (m_isGrounded)
                {
                    m_isJump = true;
                    //m_Animator.SetTrigger("Jump");
                }
            }
            //Down
            else if (gesture.ScreenFlickVector.y < -m_Angle)
            {
                m_isCrouch = true;
            }
        }
    }

    void Update()
    {
        //キャラクター変更
        CharacterChange(GameDataManager.Instance.playCharID);

        if (GameSceneManager.Instance.isGameOver ||
            !GameSceneManager.Instance.isGamePlaying)
            return;

        //プレイヤーの移動
        transform.position += new Vector3(0.0f, 0.0f, m_MoveSpeed) * Time.deltaTime * Time.timeScale;
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

        //しゃがみ中はプレイヤーキャラのColliderを無効化
        if (m_isCrouch)
        {
            m_Collider.enabled = !m_isCrouch;
            m_CharScale.y = 0.6f;
            m_TimeCount += Time.deltaTime;
            if (m_TimeCount >= m_CrouchTime)
            {
                m_TimeCount = 0.0f;
                m_isCrouch = false;
                m_Collider.enabled = !m_isCrouch;
            }
        }
        else
        {
            m_CharScale.y = 1.0f;
        }

        m_Character.transform.localScale = Vector3.Lerp(m_Character.transform.localScale, m_CharScale, 0.3f);

        //地面についている時
        if (m_isGrounded)
        {
            //横移動
            LateralMotion();
        }

        //スコア
        GameDataManager.Instance.score = (int)transform.position.z;

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

    void CharacterChange(int charID)
    {
        if (m_PreCharID != charID)
        {
            Destroy(m_Character.gameObject);

            m_PreCharID = charID;

            //パーティクル
            Instantiate(m_ChangeParticle[Random.Range(0, m_ChangeParticle.Length)], transform.position + new Vector3(0f, 0.5f, 0f), m_ChangeParticle[0].transform.rotation);

            //キャラ生成
            m_Character = Instantiate(m_AllCharacters[charID], m_CharGenPosition.position, m_CharGenPosition.rotation) as GameObject;
            m_Character.transform.SetParent(transform);

            //キャラクターのコライダー取得
            m_Collider = m_Character.GetComponent<BoxCollider>();

            if (GameDataManager.Instance.IsCharAvailable(charID))
            {
                m_Character.GetComponent<MeshRenderer>().material.color = Color.white;
            }
            else
            {
                m_Character.GetComponent<MeshRenderer>().material.color = Color.black;
            }
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
        //障害物
        if (hit.gameObject.tag == "Obstacle")
        {
            GameSceneManager.Instance.isGameOver = true;
            Instantiate(m_DieParticle, transform.position, m_DieParticle.transform.rotation);
        }

        //イカ
        if (hit.gameObject.tag == "Squid")
        {
            GameObject obj = Instantiate(m_Squid_ink[Random.Range(0, m_Squid_ink.Length)], Vector3.zero, Quaternion.identity) as GameObject;
            obj.transform.SetParent(m_Canvas.transform, false);
        }

        //コイン
        if (hit.gameObject.tag == "Coin")
        {
            Destroy(hit.gameObject);
            GameDataManager.Instance.goldCoin += 1;
        }

        //トマト
        if (hit.gameObject.tag == "Tomato")
        {
            Destroy(hit.gameObject);
        }
    }
}
