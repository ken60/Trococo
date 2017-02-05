using UnityEngine;
using UnityEngine.UI;

public class Gyro : MonoBehaviour
{
    void Start()
    {
        Input.gyro.enabled = true;
    }

    public static Vector3 GetGyroDirection()
    {
        // 端末の傾きを保持
        Quaternion dir = Input.gyro.attitude;

        // yz軸の値を入れ替え
        float temp;
        temp = dir.y;
        dir.y = dir.z;
        dir.z = temp;

        return new Vector3(dir.x * 10f, dir.y * 10f, dir.z * 10f);
    }
}
