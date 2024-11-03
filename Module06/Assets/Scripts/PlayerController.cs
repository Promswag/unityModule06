using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool _moving;
    [SerializeField] private GameObject _model;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Camera _tpsCamera;
    [SerializeField] private Camera _fpsCamera;
    [SerializeField] private PostProcessVolume _ppv;
    [SerializeField] private List<GameObject> _nearbyGhosts;
    [SerializeField] private List<AudioClip> _footSteps;

    private AudioSource _audioSource;
    private float _timeElapsed = 0;
    private float _timeToWait = 0;
    private int _footStepsIndex = 0;
    
    private Quaternion _lastRotation;
    private float _nearest;
    private Vector3 _resetPosition;
    private Quaternion _resetRotation;

    void Start()
    {
        _resetPosition = transform.position;
        _resetRotation = transform.rotation;
        _moving = false;
        _nearest = 1000f;
        GameManager.Instance._reset += ResetState;
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Vector3 newForward = new Vector3(_tpsCamera.transform.forward.x, 0f, _tpsCamera.transform.forward.z);
        transform.forward = newForward;

        DirectionHandler();

        _animator.SetBool("Moving", _moving);

        _timeElapsed += Time.deltaTime;
        if (_moving && _timeElapsed > _timeToWait)
        {
            _timeElapsed = 0;
            _audioSource.clip = _footSteps[++_footStepsIndex % _footSteps.Count];
            _timeToWait = _audioSource.clip.length + 0.025f;
            _audioSource.Play();
        }

        _nearest = 1000f;
        foreach(GameObject _ghost in _nearbyGhosts)
        {
            float _distance = Vector3.Distance(_ghost.transform.position, transform.position);
            if (_distance < _nearest)
            {
                _nearest = _distance;
            }
        }

        _ppv.profile.GetSetting<Vignette>().intensity.value = Mathf.Clamp((2f - _nearest) / 2f, 0f, 1f);
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
            _nearbyGhosts.Add(collider.gameObject);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Ghost"))
        {
            _nearbyGhosts.Remove(collider.gameObject);
        }
    }

    void ResetState()
    {
        Debug.Log("Faint");
        transform.SetPositionAndRotation(_resetPosition, _resetRotation);
    }

    void OnDestroy()
    {
        GameManager.Instance._reset -= ResetState;
    }
}
