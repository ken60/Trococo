using UnityEngine;
using UnityEngine.UI;
using NCMB;

public class DialogBox_Signin : MonoBehaviour
{
    [SerializeField]
    private GameObject m_DialogBox;
    [SerializeField]
    private Button m_Button;
    [SerializeField]
    private Text m_ButtonText;
    [SerializeField]
    private InputField m_InputField;
    [SerializeField]
    int m_PassLength = 15;

    private string ps;
    private string STR_GROUP = "0123456789ABCDEFGHIJKLNMOPQRSTUVWXYZabcdefghijklnmopqrstuvwxyz";

    void Update()
    {
        m_Button.interactable = ConfirmationUserName(m_InputField.text);
    }

    public void OK_Button()
    {
        //NCMBUserのインスタンス作成 
        NCMBUser user = new NCMBUser();
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("Ranking");

        //パスワードの生成
        ps = GenPassword();

        query.WhereEqualTo("Name", m_InputField.text);

        query.CountAsync((int count, NCMBException e) =>
        {
            if (e == null)
            {
                if (count == 0)
                {
                    //ユーザ名とパスワードの設定
                    user.UserName = m_InputField.text;
                    user.Password = ps;

                    print("Signin: " + m_InputField.text + " " + ps);

                    //会員登録を行う
                    user.SignUpAsync((NCMBException e2) =>
                    {
                        if (e2 != null)
                        {
                            GameObject dialog = Instantiate(m_DialogBox, Vector3.zero, Quaternion.identity) as GameObject;
                            dialog.transform.SetParent(GameObject.Find("Canvas").transform, false);
                            dialog.GetComponent<DialogBox>().SetText("エラー\nもう一度試してみてください");
                        }
                        else
                        {
                            Debug.Log("新規登録に成功");

                            GameDataManager.Instance.userName = m_InputField.text;
                            GameDataManager.Instance.userPass = ps;
                            GameDataManager.Instance.SaveGame();

                            GameObject dialog = Instantiate(m_DialogBox, Vector3.zero, Quaternion.identity) as GameObject;
                            dialog.transform.SetParent(GameObject.Find("Canvas").transform, false);
                            dialog.GetComponent<DialogBox>().SetText("ユーザーを作成しました！");

                            iTweenManager.Hide_ScaleTo(this.gameObject, 0.2f, "EndAction", this.gameObject);
                        }
                    });
                }
                else
                {
                    GameObject dialog = Instantiate(m_DialogBox, Vector3.zero, Quaternion.identity) as GameObject;
                    dialog.transform.SetParent(GameObject.Find("Canvas").transform, false);
                    dialog.GetComponent<DialogBox>().SetText("すでに登録されている名前です");
                }
            }
        });
    }

    bool ConfirmationUserName(string name)
    {
        if (0 < name.Length && name.Length <= 10)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    string GenPassword()
    {
        char[] list = new char[m_PassLength];
        for (int i = 0; i < m_PassLength; i++)
        {
            int num = Random.Range(0, STR_GROUP.Length); // ランダムに０～ｎの数値を返す
            list[i] = STR_GROUP[num];
        }

        return new string(list);
    }

    void EndAction()
    {
        Destroy(this.gameObject);
    }
}
