using System.Collections;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.AI;

public class GhostController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _self;
    private Vector3 _resetPosition;
    private Vector3 _target;

    private enum GhostState {
        PATROL,
        CHASE,
        THINK,
        RESET
    }

    private GhostState _state;

    void Awake()
    {
        _resetPosition = transform.position;
        _state = GhostState.PATROL;

        GargoyleController._alert += MoveTowards;
    }

    void Update()
    {
        if (_state == GhostState.PATROL)
        {
            //TODO
        }
        else if (_state == GhostState.CHASE)
        {
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

    void OnTriggerStay(Collider collider)
    {
        _self.SetDestination(collider.transform.position);
        _state = GhostState.CHASE;
        StopCoroutine(Idle());
    }

    void OnTriggerExit(Collider collider)
    {
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
    }
}
