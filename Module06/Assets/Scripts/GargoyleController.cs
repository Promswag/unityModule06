using System;
using System.Collections;
using UnityEngine;

public class GargoyleController : MonoBehaviour
{
    [SerializeField] private SphereCollider _detectionArea;
    [SerializeField] private SkinnedMeshRenderer _meshRenderer;
    private Material _light;
    private Color _lightOnColor;
    private Color _lightOffColor;
    static public event Action<Vector3> _alert;

    void Start()
    {
        _light = _meshRenderer.material;
        _lightOnColor = _light.color;
        _lightOffColor = new Color(0, 0, 0, 0);
    }

    void OnTriggerEnter(Collider collider)
    {
        StartCoroutine(Cooldown(collider.transform.position));
    }

    private IEnumerator Cooldown(Vector3 target)
    {
        _alert?.Invoke(target);
        _detectionArea.enabled = false;
        _light.color = _lightOffColor;
        yield return new WaitForSeconds(3f);
        _light.color = _lightOnColor;
        _detectionArea.enabled = true;
    }
}
