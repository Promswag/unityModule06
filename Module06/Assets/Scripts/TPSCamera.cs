using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    // [SerializeField] private Transform player;
    [SerializeField] private Transform _head;
    private Vector3 orbitOffset;

    void Start()
    {
        orbitOffset = transform.position - _head.position;
        orbitOffset.y = 0.1f;
    }

    void Update()
    {
        orbitOffset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 1f, Vector3.up) * orbitOffset;
        transform.position = _head.position + orbitOffset;
        transform.LookAt(_head.position);
    }
}
