using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    // [SerializeField] private Transform player;
    [SerializeField] private Transform _head;
    private Vector3 orbitOffset;

    [SerializeField] [Range(1f, 10f)] private float _cameraSpeed = 1f;

    void Start()
    {
        orbitOffset = transform.position - _head.position;
        orbitOffset.y = 0.1f;
    }

    void Update()
    {
        orbitOffset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * _cameraSpeed, Vector3.up) * orbitOffset;
        transform.position = Vector3.Lerp(transform.position, _head.position + orbitOffset, 0.1f);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_head.position - transform.position), 0.1f);
        // transform.LookAt(_head.position);
    }
}
