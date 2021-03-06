﻿using UnityEngine;
using UnityEngine.UI;
using NCMB;

public class DialogBox_Signin : MonoBehaviour
{
    [SerializeField]
    private GameObject m_DialogBox;
    [SerializeField]
    private Button m_Button;
    [SerializeField]
    private InputField m_InputField;
    [SerializeField]
    int m_PassLength = 15;

    private string ps;
    private string STR_GROUP = "0123456789ABCDEFGHIJKLNMOPQRSTUVWXYZabcdefghijklnmopqrstuvwxyz";

    void Update()
    {
        transform.SetAsLastSibling();
        m_Button.interactable = ConfirmationUserName(m_InputField.text);
    }

    public void OK_Button()
    {
        string name = m_InputField.text;

        Loading.Instance.ShowLoading(new Vector2(0f, 0f));

        m_Button.enabled = false;

        //NCMBUserのインスタンス作成 
        NCMBUser user = new NCMBUser();
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("Ranking");

        //パスワードの生成
        ps = GenPassword();

        query.WhereEqualTo("Name", name);

        query.CountAsync((int count, NCMBException e) =>
        {
            if (e == null)
            {
                if (count == 0)
                {
                    //ユーザ名とパスワードの設定
                    user.UserName = name;
                    user.Password = ps;

                    //会員登録を行う
                    user.SignUpAsync((NCMBException e2) =>
                    {
                        if (e2 != null)
                        {
                            Loading.Instance.HideLoading();
                            m_Button.interactable = true;

                            GameObject dialog = Instantiate(m_DialogBox, Vector3.zero, Quaternion.identity) as GameObject;
                            dialog.transform.SetParent(GameObject.Find("Canvas").transform, false);
                            dialog.GetComponent<DialogBox>().SetMessage("エラー\nもう一度試してみてください");
                        }
                        else
                        {
                            Loading.Instance.HideLoading();
                            m_Button.interactable = true;

                            AccountManager.Instance.userName = name;
                            AccountManager.Instance.userPass = ps;
                            AccountManager.Instance.SaveAccount();

                            iTweenManager.Hide_ScaleTo(this.gameObject, 0.2f, "EndAction", this.gameObject);


                            GameObject dialog = Instantiate(m_DialogBox, Vector3.zero, Quaternion.identity) as GameObject;
                            dialog.transform.SetParent(GameObject.Find("Canvas").transform, false);
                            dialog.GetComponent<DialogBox>().SetMessage("ユーザーを作成しました");
                        }
                    });
                }
                else
                {
                    Loading.Instance.HideLoading();
                    m_Button.interactable = true;

                    GameObject dialog = Instantiate(m_DialogBox, Vector3.zero, Quaternion.identity) as GameObject;
                    dialog.transform.SetParent(GameObject.Find("Canvas").transform, false);
                    dialog.GetComponent<DialogBox>().SetMessage("すでに登録されている名前です");
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
