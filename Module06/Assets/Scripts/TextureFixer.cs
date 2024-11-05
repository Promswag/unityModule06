using UnityEngine;

public class TextureFixer : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public bool refresh = false;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.SetTextureScale("_MainTex", new(transform.localScale.x, transform.localScale.y));
        meshRenderer.material.SetTextureOffset("_MainTex", new(transform.position.x - 0.5f * transform.localScale.x, transform.position.z - 0.5f * transform.localScale.y));
    }
}
