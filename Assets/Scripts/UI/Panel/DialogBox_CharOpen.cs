using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogBox_CharOpen : MonoBehaviour
{
    [SerializeField]
    private Image m_CharImage;
    [SerializeField]
    private Sprite[] m_CharSprite;
    [SerializeField]
    private GameObject m_OpenParticle;

    void Start()
    {
        transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
    }

    public void SetData(int charId)
    {
        iTweenManager.Show_ScaleTo(this.gameObject, 0.2f);

        m_CharImage.sprite = m_CharSprite[charId];

        if (GameDataManager.Instance.IsCharAvailable(charId))
        {
            //パーティクル
            //Instantiate(m_OpenParticle, Vector3.zero, Quaternion.identity);
        }
    }

    void Update()
    {
        transform.SetAsLastSibling();
    }

    public void Button_OK()
    {
        iTweenManager.Hide_ScaleTo(this.gameObject, 0.2f, "EndAction", this.gameObject);
    }

    void EndAction()
    {
        Destroy(this.gameObject);
    }
}