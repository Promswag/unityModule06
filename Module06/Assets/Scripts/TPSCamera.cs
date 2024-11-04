using System;
using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    [SerializeField] private Transform _head;
    private Vector3 _orbitOffset;
    private float _maxDistance;
    [SerializeField] [Range(0.1f, 1f)] private float _minDistance;
    [SerializeField] [Range(0.1f, 1f)] private float _lerpSmooth;
    [SerializeField] [Range(0.1f, 10f)] private float _cameraSpeed = 1f;

    void Awake()
    {
        _orbitOffset = transform.position - _head.position;
        // _orbitOffset.y = 0.1f;
        _maxDistance = Vector3.Distance(transform.position, _head.transform.position);
    }

    void Update()
    {
        _orbitOffset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * _cameraSpeed, Vector3.up) * _orbitOffset;
        transform.position = Vector3.Lerp(transform.position, _head.position + _orbitOffset, _lerpSmooth);
        // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_head.position - transform.position), _lerpSmooth);
        Vector3 dir = CollisionHandler();
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-dir), _lerpSmooth);
    }

    Vector3 CollisionHandler()
    {
        Vector3 _dir = (transform.position - _head.position).normalized;
        float _distance = _maxDistance;
        if (Physics.SphereCast(_head.position, 0.01f, _dir, out RaycastHit hit, _maxDistance, 1 << LayerMask.NameToLayer("Structure")))
        {
            if (hit.distance < _minDistance)
            {
                _distance = -_minDistance;
            }
            else 
            {
                _distance = Mathf.Clamp(hit.distance, _minDistance, _maxDistance);
            }
        }
        transform.position = Vector3.Lerp(transform.position, _head.position + _dir * _distance, 0.5f);
        return _dir;
    }
}
