using UnityEngine;

public class NetworkChecker : SingletonMonoBehaviour<NetworkChecker>
{
    [SerializeField]
    private GameObject m_NetworkDalog;
    [SerializeField]
    private GameObject m_Canvas;

    private NetworkReachability networkReachability;
    private GameObject m_instanteObj;
    private bool isShowDialog = false;

    private void Start()
    {
        //m_Canvas = GameObject.Find("Canvas");
        //print("m_Canvas " + m_Canvas.name);
    }

    private void Update()
    {
        if (networkReachability != Application.internetReachability)
        {
            networkReachability = Application.internetReachability;

            ReachableNetwork();
        }
    }

    public void ReachableCheck()
    {
        switch (Application.internetReachability)
        {
            case NetworkReachability.NotReachable:
                if (!isShowDialog)
                {
                    isShowDialog = true;

                    m_instanteObj = Instantiate(m_NetworkDalog, Vector3.zero, Quaternion.identity) as GameObject;
                    m_instanteObj.transform.SetParent(m_Canvas.transform, false);
                    m_instanteObj.GetComponent<DialogBox>().SetMessage("通信エラーが発生しました。\nインターネット接続を確認してください。");
                    m_instanteObj.GetComponent<DialogBox>().m_isShowButton = false;

                    iTweenManager.Show_ScaleTo(m_instanteObj, 0.2f);

                    Time.timeScale = 0f;
                }
                break;

            case NetworkReachability.ReachableViaCarrierDataNetwork:
                break;

            case NetworkReachability.ReachableViaLocalAreaNetwork:
                break;

            default:
                break;
        }
    }

    void ReachableNetwork()
    {
        if (isShowDialog)
        {
            isShowDialog = false;
            iTweenManager.Hide_ScaleTo(m_instanteObj, 0.2f, "EndAction", this.gameObject);
            Time.timeScale = 1f;
        }
    }

    void EndAction()
    {
        if (m_instanteObj != null)
            Destroy(m_instanteObj);
    }
}