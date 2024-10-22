using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool _moving;
    [SerializeField] private GameObject _model;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Camera _camera;

    void Start()
    {
        _moving = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            _moving = true;
        }
        else
        {
            _moving = false;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            _model.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _model.transform.rotation = Quaternion.Euler(0f, 270f, 0f);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _model.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _model.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }


        if (_moving)
        {
            _animator.SetFloat("Speed", 1.0f);
            // _rigidbody.MovePosition(transform.forward);
        }
        else
        {
            _animator.SetFloat("Speed", 0.0f);
        }
    }
}
