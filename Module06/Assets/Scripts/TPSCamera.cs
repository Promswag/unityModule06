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
        orbitOffset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 5f, Vector3.up) * orbitOffset;
        transform.position = Vector3.Lerp(transform.position, _head.position + orbitOffset, 0.10f);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_head.position - transform.position), 0.1f);
        // transform.LookAt(_head.position);
    }
}
