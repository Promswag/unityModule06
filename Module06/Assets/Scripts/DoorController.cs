using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator _doorAnimator;
    private bool _isActivated = false;

    void OnTriggerStay()
    {
        if (Input.GetKeyDown(KeyCode.E) && !_isActivated)
        {
            _isActivated = true;
            _doorAnimator.SetTrigger("DoorTrigger");
        }
    }

    void Available()
    {
        _isActivated = false;
    }
}
