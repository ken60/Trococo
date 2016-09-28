using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogBox_UserRegister : MonoBehaviour
{
    [SerializeField]
    private InputField m_InputField;
    [SerializeField]
    private GameObject m_Dialog;

    private GameObject m_Canvas;
    private string PASSWORD_STR = "0123456789ABCDEFGHIJKLNMOPQRSTUVWXYZabcdefghijklnmopqrstuvwxyz";

    void Start()
    {
        m_Canvas = GameObject.Find("Canvas");
    }

    public void Button_OK()
    {
        if (m_InputField.text == "")
        {
            Debug.Log("名前が空白");
            GameObject dialog = Instantiate(m_Dialog, Vector3.zero, Quaternion.identity) as GameObject;
            dialog.transform.SetParent(m_Canvas.transform, false);
            dialog.GetComponent<DialogBox>().SetText("名前を入力してください");
        }
        else
        {
            StartCoroutine(SignUpWaiting());
        }
    }

    private IEnumerator SignUpWaiting()
    {
        string password = PassWordGen(15);

        NCMB_UserRegister.Instance.SignUp(m_InputField.text, password);

        while (true)
        {
            yield return null;

            if (NCMB_UserRegister.Instance.eroorCode == NCMB_UserRegister.SignUpState.NameDuplicate)
            {
                Debug.Log("名前が重複");
                GameObject dialog = Instantiate(m_Dialog, m_Dialog.transform.position, Quaternion.identity) as GameObject;
                dialog.transform.SetParent(m_Canvas.transform, false);
                dialog.GetComponent<DialogBox>().SetText("すでに登録されている名前です");

                NCMB_UserRegister.Instance.eroorCode = NCMB_UserRegister.SignUpState.Null;
            }
            else if (NCMB_UserRegister.Instance.isRegistCompletion)
            {
                GameSettingsManager.Instance.userID = m_InputField.text;
                GameSettingsManager.Instance.password = password;
                GameSettingsManager.Instance.SaveSettings();

                iTweenManager.Hide_ScaleTo(this.gameObject, 0.2f, "EndAction", this.gameObject);

                Debug.Log("登録完了");
                break;
            }
        }
    }

    //パスワード生成
    public string PassWordGen(int count)
    {
        char[] list = new char[count];
        for (int i = 0; i < count; i++)
        {
            int num = Random.Range(0, PASSWORD_STR.Length);
            list[i] = PASSWORD_STR[num];
        }
        return new string(list);
    }

    void EndAction()
    {
        GameDataManager.Instance.SaveGame();
        Destroy(this.gameObject);
    }
}