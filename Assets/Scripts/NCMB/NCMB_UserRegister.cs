using UnityEngine;
using NCMB;

public class NCMB_UserRegister : SingletonMonoBehaviour<NCMB_UserRegister>
{
    public enum SignUpState
    {
        Null = 0,       //初期状態
        Completion,     //登録完了
        NameDuplicate,  //名前が重複
    }

    private SignUpState m_ErrorCode = SignUpState.Null;
    private bool m_isRegComp = false;


    //mobile backendに接続してログイン ------------------------
    public void LogIn(string id, string pw)
    {
        NCMBUser.LogInAsync(id, pw, (NCMBException e) =>
        {
            // 接続成功したら
            if (e == null)
            {
                Debug.Log("接続成功");
            }
        });
    }

    //mobile backendに接続して新規会員登録 ------------------------
    public void SignUp(string id, string pw)
    {
        NCMBUser user = new NCMBUser();
        user.UserName = id;
        user.Password = pw;
        user.SignUpAsync((NCMBException e) =>
        {
            Debug.Log("ErrorCode: " + e.ErrorCode);

            //名前が重複
            if (e.ErrorCode == "E409001")
            {
                m_ErrorCode = SignUpState.NameDuplicate;
            }
            //登録完了
            else if (e.ErrorCode == null)
            {
                m_ErrorCode = SignUpState.Completion;
                m_isRegComp = true;
            }
        });
    }

    //mobile backendに接続してログアウト ------------------------
    public void LogOut()
    {
        NCMBUser.LogOutAsync((NCMBException e) =>
        {
            if (e == null)
            {
                Debug.Log("ログアウト成功");
            }
        });
    }

    //登録に成功したか
    public bool isRegistCompletion
    {
        get { return m_isRegComp; }
    }

    //エラーコード
    public SignUpState eroorCode
    {
        get { return m_ErrorCode; }
        set { m_ErrorCode = value; }
    }
}