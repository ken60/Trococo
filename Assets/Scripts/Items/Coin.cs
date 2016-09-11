using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private float m_RotateSpeed;

    void Update()
    {
        transform.Rotate(new Vector3(0.0f, m_RotateSpeed * Time.deltaTime * Time.timeScale, 0.0f));
    }
}