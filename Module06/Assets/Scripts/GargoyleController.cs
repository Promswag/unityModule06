using System;
using System.Collections;
using UnityEngine;

public class GargoyleController : MonoBehaviour
{
    [SerializeField] private Material _light;
    [SerializeField] private SphereCollider _detectionArea;
    static public event Action<Vector3> _alert;
    private Color _lightDefaultColor;
    private Color _lightOff;

    void Start()
    {
        _lightDefaultColor = _light.color;
        _lightOff = new Color(0, 0, 0, 0);
    }

    void OnTriggerEnter(Collider collider)
    {
        StartCoroutine(Cooldown(collider.transform.position));
    }

    private IEnumerator Cooldown(Vector3 target)
    {
        _alert?.Invoke(target);
        _detectionArea.enabled = false;
        _light.color = _lightOff;
        yield return new WaitForSeconds(3f);
        _light.color = _lightDefaultColor;
        _detectionArea.enabled = true;
    }
}
