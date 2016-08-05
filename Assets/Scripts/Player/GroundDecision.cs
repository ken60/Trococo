using UnityEngine;

public class GroundDecision : MonoBehaviour
{
    [SerializeField]
    private bool m_isGround = false;

    public bool isGround
    {
        get { return m_isGround; }
    }
    
    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Rail")
        {
            m_isGround = true;
        }
    }

}
