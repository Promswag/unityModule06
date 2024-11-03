using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GhostController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _self;
    private Vector3 _resetPosition;
    private Vector3 _target;
    private GameObject _player;

    private enum GhostState {
        PATROL,
        CHASE,
        THINK,
        RESET
    }

    private GhostState _state;

    void Start()
    {
        _resetPosition = transform.position;
        _state = GhostState.PATROL;

        GargoyleController._alert += MoveTowards;
		GameManager.Instance._reset += ResetState;
    }

    void Update()
    {
        if (_state == GhostState.PATROL)
        {
            //TODO
        }
        else if (_state == GhostState.CHASE)
        {
            if (_player)
            {
                _target = _player.transform.position;
            }
            _self.SetDestination(_target);
            if (Vector3.Distance(transform.position, _target) < 0.1f)
            {
                StartCoroutine(Idle());
            }
        }
        else if (_state == GhostState.RESET)
        {
            if (Vector3.Distance(transform.position, _resetPosition) < 0.1f)
            {
                _state = GhostState.PATROL;
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "JohnLemon")
        {
            _player = collider.gameObject;
            _state = GhostState.CHASE;
            StopCoroutine(Idle());
        }
    }

    void OnTriggerExit(Collider collider)
    {
        _player = null;
        StartCoroutine(Idle());
    }

    IEnumerator Idle()
    {
        _state = GhostState.THINK;
        yield return new WaitForSeconds(3f);
        _state = GhostState.RESET;
        _self.SetDestination(_resetPosition);
    }

    void MoveTowards(Vector3 target)
    {
        _state = GhostState.CHASE;
        _target = new Vector3(target.x, transform.position.y, target.z);
        _self.SetDestination(_target);
    }

    void OnDestroy()
    {
        GargoyleController._alert -= MoveTowards;
        GameManager.Instance._reset -= ResetState;
    }

    void ResetState()
    {
        StopCoroutine(Idle());
        Debug.Log("On ResetState!");
        transform.position = _resetPosition;
        _state = GhostState.PATROL;
        _self.ResetPath();
    }
}
