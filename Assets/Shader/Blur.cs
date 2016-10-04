using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class Blur : MonoBehaviour
{
    [SerializeField, Range(0f, 0.01f)]
    private float m_BulrWidth = 0f;
    [SerializeField, Range(0f, 0.01f)]
    private float m_BulrHeight = 0f;
    [SerializeField]
    private Material material = null;
    [SerializeField]
    private Shader m_Shader = null;

    void Awake()
    {
        if (m_Shader == null)
        {
            m_Shader = Shader.Find("Custom/Blur");
        }

        material = new Material(m_Shader);
        material.hideFlags = HideFlags.HideAndDontSave;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
        material.SetFloat("_Width", m_BulrWidth);
        material.SetFloat("_Height", m_BulrHeight);
    }
}
