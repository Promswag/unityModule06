using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator _doorAnimator;
    [SerializeField] private AudioSource _audioSource;
    private bool _isActivated = false;
    private bool _isInTrigger = false;

    void Update()
    {
        if (_isInTrigger && !_isActivated && Input.GetKeyDown(KeyCode.E))
        {
            _isActivated = true;
            _doorAnimator.SetTrigger("DoorTrigger");
            _audioSource.Play();
        }
    }

    void OnTriggerEnter()
    {
        _isInTrigger = true;
    }
    void OnTriggerExit()
    {
        _isInTrigger = false;
    }

    public void Available()
    {
        _isActivated = false;
    }
}