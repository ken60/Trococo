using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogBox_UserRegister : MonoBehaviour
{
    [SerializeField]
    private InputField m_InputField;
    [SerializeField]
    private GameObject m_Dialog;

    private string PASSWORD_STR = "0123456789ABCDEFGHIJKLNMOPQRSTUVWXYZabcdefghijklnmopqrstuvwxyz";

    public void Button_OK()
    {
        if (m_InputField.text != "")
        {
            string password = PassWordGen(15);

            NCMB_UserRegister.Instance.SignUp(m_InputField.text, password);
            GameSettingsManager.Instance.userID = m_InputField.text;
            GameSettingsManager.Instance.password = password;
            GameSettingsManager.Instance.SaveSettings();

            iTweenManager.Hide_ScaleTo(this.gameObject, 0.2f, "EndAction", this.gameObject);
        }
        else
        {
            GameObject dialog = Instantiate(m_Dialog, Vector3.zero, Quaternion.identity) as GameObject;
            dialog.GetComponent<DialogBox>().SetText("名前を入力してください！");
            dialog.transform.SetParent(GameObject.Find("Canvas").transform, false);
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