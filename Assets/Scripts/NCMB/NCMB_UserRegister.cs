using UnityEngine;
using NCMB;
using System.Collections;

public class NCMB_UserRegister : SingletonMonoBehaviour<NCMB_UserRegister>
{
    private string currentPlayerName;

    // mobile backendに接続してログイン ------------------------

    public void LogIn(string id, string pw)
    {
        NCMBUser.LogInAsync(id, pw, (NCMBException e) =>
        {
            // 接続成功したら
            if (e == null)
            {
                currentPlayerName = id;
            }
        });
    }

    // mobile backendに接続して新規会員登録 ------------------------
    public void SignUp(string id, string pw)
    {
        NCMBUser user = new NCMBUser();
        user.UserName = id;
        user.Password = pw;
        user.SignUpAsync((NCMBException e) =>
        {
            if (e == null)
            {
                currentPlayerName = id;
            }
        });
    }

    public void SignUp(string id, string mail, string pw)
    {
        NCMBUser user = new NCMBUser();
        user.UserName = id;
        user.Email = mail;
        user.Password = pw;
        user.SignUpAsync((NCMBException e) =>
        {
            if (e == null)
            {
                currentPlayerName = id;
            }
            else
            {
                Debug.Log("登録失敗");
            }
        });
    }

    // mobile backendに接続してログアウト ------------------------

    public void LogOut()
    {
        NCMBUser.LogOutAsync((NCMBException e) =>
        {
            if (e == null)
            {
                currentPlayerName = null;
            }
        });
    }

    // 現在のプレイヤー名を返す --------------------
    public string CurrentPlayerName()
    {
        return currentPlayerName;
    }
}