using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _mouseSensivity = 1f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (Mathf.Abs(mouseX) > 0.05f)
        {
            _player.Rotate(Vector3.up * (Input.GetAxis("Mouse X") * _mouseSensivity));
        }

        if (Mathf.Abs(mouseY) > 0.05f)
        {
            transform.Rotate(Vector3.right * Mathf.Clamp(-mouseY, -90, 90));
        }
    }
}