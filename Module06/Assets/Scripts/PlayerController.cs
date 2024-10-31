using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool _moving;
    [SerializeField] private GameObject _model;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Camera _tpsCamera;
    [SerializeField] private Camera _fpsCamera;
    
    private Quaternion _lastRotation;

    void Start()
    {
        _moving = false;
    }

    void Update()
    {
        Vector3 newForward = new Vector3(_tpsCamera.transform.forward.x, 0f, _tpsCamera.transform.forward.z);
        transform.forward = newForward;

        DirectionHandler();

        if (_moving)
        {
            _animator.SetFloat("Speed", 1.0f);
        }
        else
        {
            _animator.SetFloat("Speed", 0.0f);
        }
    }

    void DirectionHandler()
    {
        _moving = true;
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0f, 45f, 0f);
        }
        else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
        {
            transform.Rotate(0f, 135f, 0f);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0f, 225f, 0f);
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
        {
            transform.Rotate(0f, 315f, 0f);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            transform.Rotate(0f, 0f, 0f);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0f, 270f, 0f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(0f, 180f, 0f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0f, 90f, 0f);
        }
        else
        {
            transform.rotation = _lastRotation;
            _moving = false;
        }
        _lastRotation = transform.rotation;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Ghost"))
        {
            Debug.Log("LOL");
        }
    }
}
