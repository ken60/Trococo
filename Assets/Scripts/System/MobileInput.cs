using UnityEngine;
using System.Collections;

public class MobileInput : MonoBehaviour
{
    private static Vector2 m_StartPos;
    private static Vector2 m_EndPos;
    private static string m_Direction;
    private static TouchType m_TouchDir;

    public enum TouchType
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        TOUCH,
        NONE
    }

    public static bool GetTouch(TouchType type)
    {
        if (GetFlick() == type) return true;
        return false;
    }

    public static TouchType GetFlick()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            m_StartPos = Input.mousePosition;
        }

        else if (Input.GetMouseButtonUp(0))
        {
            m_EndPos = Input.mousePosition;
            GetDirection();
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
                    GetDirection();
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


    private static void GetDirection()
    {
        Vector2 direction = m_EndPos - m_StartPos;

        if (Mathf.Abs(direction.y) < Mathf.Abs(direction.x))
        {
            if (30 < direction.x)
            {
                //右向きにフリック
                m_TouchDir = TouchType.RIGHT;
            }
            else if (-30 > direction.x)
            {
                //左向きにフリック
                m_TouchDir = TouchType.LEFT;
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (30 < direction.y)
            {
                //上向きにフリック
                m_TouchDir = TouchType.UP;
            }
            else if (-30 > direction.y)
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