using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField]
    private float m_MoveSpeed;   //プレイヤーの移動速度

    void Start ()
	{
	
	}
	
	void Update ()
	{

        if (GameSceneManager.Instance.isGameOver || !GameSceneManager.Instance.isGamePlaying) return;

        //プレイヤーの移動
        transform.position += new Vector3(0.0f, 0.0f, m_MoveSpeed) * Time.deltaTime;
        //m_Rigidbody.velocity = (Vector3.forward * m_MoveSpeed) *Time.deltaTime;
    }
}