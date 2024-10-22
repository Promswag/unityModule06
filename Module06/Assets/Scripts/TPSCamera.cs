using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    // [SerializeField] private Transform player;
    [SerializeField] private Transform head;
    private Vector3 orbitOffset;

    void Start()
    {
        orbitOffset = transform.position - head.position;
        orbitOffset.y = 0.1f;
    }

    void Update()
    {
        orbitOffset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 1f, Vector3.up) * orbitOffset;
        transform.position = head.position + orbitOffset;
        transform.LookAt(head.position);
    }
}
