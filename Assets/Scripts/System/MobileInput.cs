using UnityEngine;
using System.Collections;

public class MobileInput : MonoBehaviour
{
    public static MobileInput Instance { get { return instance; } }

    [SerializeField]
    private float m_Angle;

    private static MobileInput instance;
    private Vector2 m_StartPos;
    private Vector2 m_EndPos;
    private TouchType m_TouchDir;
    private string m_Direction;

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

    private TouchType GetFlick()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            m_StartPos = Input.mousePosition;
        }

        else if (Input.GetMouseButtonUp(0))
        {
            m_EndPos = Input.mousePosition;
            Vector2 direction = m_EndPos - m_StartPos;
            GetDirection(direction);
        }
        else
        {
            m_TouchDir = TouchType.NONE;
        }
#elif UNITY_IOS || UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                //最初にタッチされた座標を取得
                case TouchPhase.Began:
                    m_StartPos = touch.position;
                    break;

                //指を離した座標を取得
                case TouchPhase.Ended:
                    m_EndPos = touch.position;
                    Vector2 direction = m_EndPos - m_StartPos;
                    GetDirection(direction);
                    break;
            }
        }
        else
        {
            m_TouchDir = TouchType.NONE;
        }
#endif
        return m_TouchDir;
    }


    private void GetDirection(Vector2 direction)
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
        else
        {
            //タッチを検出
            m_TouchDir = TouchType.TOUCH;
        }
    }
}