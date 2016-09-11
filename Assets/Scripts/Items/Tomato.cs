using UnityEngine;
using System.Collections;
public class Tomato : MonoBehaviour
{
    [SerializeField]
    private float m_RotateSpeed;

    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(new Vector3(0.0f, m_RotateSpeed * Time.deltaTime * Time.timeScale, 0.0f));
    }
}