using UnityEngine;
using UnityEngine.UI;

public class MobileInput : MonoBehaviour
{
    public static MobileInput Instance { get { return instance; } }

    [SerializeField]
    private float m_Angle;
    [SerializeField]
    private float m_TapRange = 0.0f;
    [SerializeField]
    private Slider m_Slider;

    private static MobileInput instance;
    private Vector2 m_StartPos = Vector2.zero;
    private Vector2 m_EndPos = Vector2.zero;
    private TouchType m_TouchDir = TouchType.NONE;
    private string m_Direction = null;
    private bool m_DuringTap = false;

    public enum TouchType
    {
        NONE = 0,
        UP,
        DOWN,
        LEFT,
        RIGHT,
        TOUCH
    }

    private void Start()
    {
        instance = this;
    }


    public bool GetTouchType(TouchType type)
    {
        if (GetFlick() == type) return true;
        return false;
    }

    public bool IsFlickUp()
    {
        if (GetFlick() == TouchType.UP) return true;
        return false;
    }

    public bool IsFlickDown()
    {
        if (GetFlick() == TouchType.DOWN) return true;
        return false;
    }

    public bool IsFlickLeft()
    {
        if (GetFlick() == TouchType.LEFT) return true;
        return false;
    }

    public bool IsFlickRight()
    {
        if (GetFlick() == TouchType.RIGHT) return true;
        return false;
    }

    public bool IsTouch()
    {
        if (GetFlick() == TouchType.TOUCH) return true;
        return false;
    }

    public bool duringTap
    {
        get { return m_DuringTap; }
    }


    private TouchType GetFlick()
    {
        //Unity Editorの場合
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_StartPos = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                m_EndPos = Input.mousePosition;
                GetDirection(m_StartPos, m_EndPos);
            }
            else
            {
                m_TouchDir = TouchType.NONE;
            }
        }
        //Androidの場合
        else if (Application.platform == RuntimePlatform.Android)
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    //最初にタッチされた座標を取得
                    case TouchPhase.Began:
                        m_DuringTap = true;
                        m_StartPos = touch.position;
                        break;

                    //指を離した座標を取得
                    case TouchPhase.Ended:
                        m_DuringTap = false;
                        m_EndPos = touch.position;
                        GetDirection(m_StartPos, m_EndPos);
                        break;
                }
            }
            else
            {
                m_TouchDir = TouchType.NONE;
            }

        return m_TouchDir;
    }


    private void GetDirection(Vector2 start, Vector2 end)
    {
        m_TapRange = (int)m_Slider.value;
        Vector2 direction = end - start;

        //m_TapRange = 15ぐらい？
        if (m_TapRange < Vector2.Distance(start, end))
        {
            if (Mathf.Abs(direction.y) < Mathf.Abs(direction.x))
            {
                if (m_Angle < direction.x)
                {
                    //右向きにフリック
                    m_TouchDir = TouchType.RIGHT;
                }
                else if (-m_Angle > direction.x)
                {
                    //左向きにフリック
                    m_TouchDir = TouchType.LEFT;
                }
            }
            else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
            {
                if (m_Angle < direction.y)
                {
                    //上向きにフリック
                    m_TouchDir = TouchType.UP;
                }
                else if (-m_Angle > direction.y)
                {
                    //下向きのフリック
                    m_TouchDir = TouchType.DOWN;
                }
            }
        }
        else
        {
            m_TouchDir = TouchType.TOUCH;
        }
    }
}