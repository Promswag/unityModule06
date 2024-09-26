using UnityEngine;

public class TextureTilingOffset : MonoBehaviour
{
    [SerializeField] private Vector2 tiling = new();
    [SerializeField] private Vector2 offset = new();
    [SerializeField] private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer.material.SetTextureScale("_MainTex", tiling);
        meshRenderer.material.SetTextureOffset("_MainTex", offset);
    }
}
