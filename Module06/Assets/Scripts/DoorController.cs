using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator _doorAnimator;
    private AudioSource _audioSource;

    // [SerializeField] private List<AudioClip> _openDoorClips;
    [SerializeField] private AudioClip _unlockDoorClip;
    [SerializeField] private AudioClip _lockedDoorClip;
    [SerializeField] private AudioClip _openDoorActiveClip;

    [SerializeField] private bool _isLocked = false;
    private bool _isActivated = false;
    private bool _isInTrigger = false;

    void Start()
    {
        _doorAnimator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        // _openDoorActiveClip = _openDoorClips[Random.Range(0, _openDoorClips.Count)];
        // DoorTransitionInit();
    }

    void Update()
    {
        if (_isInTrigger && !_isActivated && Input.GetKeyDown(KeyCode.E))
        {  
            if (!_isLocked)
            {
                _isActivated = true;
                _audioSource.clip = _openDoorActiveClip;
                _doorAnimator.SetTrigger("DoorTrigger");
            } 
            else 
            {
                if (GameManager.Instance.HasEnoughKeys())
                {
                    _audioSource.clip = _unlockDoorClip;
                    _isLocked = false;
                }
                else 
                {
                    _audioSource.clip = _lockedDoorClip;
                }
            }
            _audioSource.Play();
        }
    }

    // void DoorTransitionInit()
    // {
    //     AnimatorOverrideController _overrideController = new (_doorAnimator.runtimeAnimatorController);
    //     _doorAnimator.runtimeAnimatorController = _overrideController;
    //     foreach (ChildAnimatorState state in (_overrideController.runtimeAnimatorController as AnimatorController).layers[0].stateMachine.states)
    //     {
    //         foreach (AnimatorStateTransition transition in state.state.transitions)
    //         {
    //             if (transition.name == "open" || transition.name == "close")
    //             {
    //                 transition.duration = _openDoorActiveClip.length;
    //             }
    //         }
    //     }
    //     _doorAnimator.Rebind();
    // }

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