using UnityEngine;

[ExecuteInEditMode]
public class TextureTilingOffset : MonoBehaviour
{
    [SerializeField] private Vector2 tiling = new();
    [SerializeField] private Vector2 offset = new();
    [SerializeField] private MeshRenderer meshRenderer;

    void Start()
    {
        // meshRenderer = GetComponent<MeshRenderer>();
        // tiling = new(1, 1);
        // offset = new(0, 0);
    }

    void Update()
    {
        meshRenderer.material.SetTextureScale("_MainTex", tiling);
        meshRenderer.material.SetTextureOffset("_MainTex", offset);
    }
}
