using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator _doorAnimator;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _openDoor;
    [SerializeField] private AudioClip _unlockDoor;
    [SerializeField] private AudioClip _lockedDoor;
    private bool _isActivated = false;
    private bool _isInTrigger = false;
    [SerializeField] private bool _isLocked = false;

    void Update()
    {
        if (_isInTrigger && !_isActivated && Input.GetKeyDown(KeyCode.E))
        {
            if (!_isLocked)
            {
                _isActivated = true;
                _doorAnimator.SetTrigger("DoorTrigger");
                _audioSource.clip = _openDoor;
            } 
            else 
            {
                if (GameManager.Instance.HasEnoughKeys())
                {
                    // Debug.Log("The Door is locked! Find all the keys!");
                    _audioSource.clip = _unlockDoor;
                    _isLocked = false;
                }
                else 
                {
                    // Debug.Log("The Door has been unlocked!");
                    _audioSource.clip = _lockedDoor;
                }
            }
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
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2.9f);
        _isActivated = false;
    }
}