using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    public bool m_isShowButton = true;

    [SerializeField]
    private Text m_Text;
    [SerializeField]
    private Button m_Button;

    private string m_DescriptionText;

    void Start()
    {
        transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);

        m_Text.text = "Please set using the SetText(string)";

        iTweenManager.Show_ScaleTo(this.gameObject, 0.2f);

    }

    void Update()
    {
        m_Text.text = m_DescriptionText;
        m_Button.gameObject.SetActive(m_isShowButton);
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

    public void SetMessage(string text)
    {
        m_DescriptionText = text;
    }
}