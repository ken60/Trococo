using UnityEngine;
using System.Collections;
using System.Linq;

public class CharcterView : MonoBehaviour
{

    /// <summary>回転対象</summary>
    public GameObject Cube;
    /// <summary>回転速度</summary>
    public float Speed = 0.01f;

    void Update()
    {
        //タッチ数取得(Linq使えた)
        int touchCount = Input.touches
            .Count(t => t.phase != TouchPhase.Ended && t.phase != TouchPhase.Canceled);

        if (touchCount == 1)
        {
            Touch t = Input.touches.First();
            switch (t.phase)
            {
                case TouchPhase.Moved:

                    //移動量に応じて角度計算
                    float xAngle = t.deltaPosition.y * Speed * 10;
                    float yAngle = -t.deltaPosition.x * Speed * 10;
                    float zAngle = 0;

                    //回転
                    Cube.transform.Rotate(xAngle, yAngle, zAngle, Space.World);

                    break;
            }

        }
    }
}