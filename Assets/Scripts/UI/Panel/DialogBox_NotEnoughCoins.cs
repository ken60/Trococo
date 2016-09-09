using UnityEngine;
using UnityEngine.UI;

public class DialogBox_NotEnoughCoins : MonoBehaviour
{
    [SerializeField]
    private Text m_Text;
    [SerializeField]
    private int m_FontSize = 64;
    [SerializeField, TextArea(5, 14)]
    private string m_Content = null;
    
    void Start()
    {
        transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);

        m_Text.text = m_Content;
        m_Text.fontSize = m_FontSize;

        iTweenManager.Show_ScaleTo(this.gameObject, 0.2f);
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