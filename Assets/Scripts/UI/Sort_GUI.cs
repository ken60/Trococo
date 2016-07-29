using UnityEngine;

public class Sort_GUI : MonoBehaviour
{
    [SerializeField, Tooltip("描画順番。大きいほど手前に")]
    private int index = 0;

    void Update()
    {
        transform.SetSiblingIndex(index);
    }
}