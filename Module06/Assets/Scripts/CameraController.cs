using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _fpsCamera;
    [SerializeField] private Camera _tpsCamera;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _fpsCamera.enabled = !_fpsCamera.enabled;
            _tpsCamera.enabled = !_tpsCamera.enabled;
        }
    }
}
