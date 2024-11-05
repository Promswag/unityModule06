using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _fpsCamera;
    [SerializeField] private Camera _tpsCamera;
    [SerializeField] private SkinnedMeshRenderer _smr;
    private Mesh _savedMesh;

    void Start()
    {
        _savedMesh = _smr.sharedMesh;
        _smr.sharedMesh = _tpsCamera.enabled ? _savedMesh : null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _fpsCamera.enabled = !_fpsCamera.enabled;
            _tpsCamera.enabled = !_tpsCamera.enabled;
            _smr.sharedMesh = _tpsCamera.enabled ? _savedMesh : null;
        }
    }
}
