using System;
using System.Collections;
using UnityEngine;

public class GargoyleController : MonoBehaviour
{
    [SerializeField] private Material _light;
    [SerializeField] private SphereCollider _detectionArea;
    static public event Action<Vector3> _alert;
    private Color _lightDefaultColor;

    void Start()
    {
        _lightDefaultColor = _light.color;
    }

    void OnTriggerEnter(Collider collider)
    {
        StartCoroutine(Cooldown(collider.transform.position));
    }

    private IEnumerator Cooldown(Vector3 target)
    {
        _alert?.Invoke(target);
        _detectionArea.enabled = false;
        // _light.EnableKeyword("_EMISSION");
        _light.color = new Color(0, 0, 0, 0);
        yield return new WaitForSeconds(3f);
        // _light.DisableKeyword("_EMISSION");
        _light.color = _lightDefaultColor;
        _detectionArea.enabled = true;
    }
}
