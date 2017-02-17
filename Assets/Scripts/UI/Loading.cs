using UnityEngine;
using UnityEngine.UI;

public class Loading : SingletonMonoBehaviour<Loading>
{
    private Image loadImg;

    private void Start()
    {
        loadImg = GetComponent<Image>();
        HideLoading();
    }

    public void ShowLoading(Vector2 pos)
    {
        loadImg.transform.localPosition = pos;
        loadImg.enabled = true;
    }

    public void HideLoading()
    {
        loadImg.enabled = false;
    }
}
