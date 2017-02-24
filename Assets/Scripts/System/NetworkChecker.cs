using UnityEngine;

public class NetworkChecker : SingletonMonoBehaviour<NetworkChecker>
{
    [SerializeField]
    private GameObject m_NetworkDalog;

    private NetworkReachability _networkReachability;
    private GameObject m_instanteObj;
    private bool isShowDialog = false;

    private void Update()
    {
        if (_networkReachability != Application.internetReachability)
        {
            _networkReachability = Application.internetReachability;

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

                    Loading.Instance.HideLoading();

                    m_instanteObj = TRC_Utility.CanvasInstantilate(m_NetworkDalog, Vector3.zero, Quaternion.identity);
                    m_instanteObj.transform.SetAsLastSibling();
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

    public NetworkReachability networkReachability
    {
        get { return _networkReachability; }
    }
}