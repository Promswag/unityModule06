using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _mouseSensivity = 1f;
    private Quaternion _resetRotation;

    void Start()
    {
        _resetRotation = transform.rotation;
        GameManager.Instance._reset += ResetState;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (Mathf.Abs(mouseX) > 0.01f)
        {
            _player.Rotate(Vector3.up * (mouseX * _mouseSensivity));
        }

        if (Mathf.Abs(mouseY) > 0.01f)
        {
            transform.Rotate(Vector3.right * -mouseY);
            Vector3 _eulerAngles = transform.rotation.eulerAngles;
            if (_eulerAngles.x > 45 && _eulerAngles.x < 315)
            {
                _eulerAngles.x = _eulerAngles.x > 180 ? 315 : 45;
            }
            transform.rotation = Quaternion.Euler(_eulerAngles.x, _eulerAngles.y, 0);
        }
    }

    void ResetState()
    {
        transform.rotation = _resetRotation;
    }
}