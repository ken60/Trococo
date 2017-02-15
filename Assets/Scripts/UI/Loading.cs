using UnityEngine;
using UnityEngine.UI;

public class Loading : SingletonMonoBehaviour<Loading>
{
    [SerializeField]
    private GameObject loadImg;

    private GameObject instanceObj;
    private GameObject canvas;

    private void Start()
    {
        canvas = GameObject.Find("Canvas");
    }

    public void ShowLoading(Vector2 pos)
    {
        loadImg.transform.localPosition = pos;
        instanceObj = Instantiate(loadImg, pos, Quaternion.identity) as GameObject;
        instanceObj.transform.SetParent(canvas.transform, false);
    }

    public void HideLoading()
    {
        Destroy(instanceObj);
    }
}
