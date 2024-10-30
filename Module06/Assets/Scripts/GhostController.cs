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
        Debug.Log(gameObject.name + " " + _resetPosition);
    }

    void Update()
    {
        if (_state == GhostState.PATROL)
        {
            Debug.Log("A");
            //TODO
        }
        else if (_state == GhostState.CHASE)
        {
            Debug.Log("B");
            if (Vector3.Distance(transform.position, _target) < 0.1f)
            {
                Debug.Log("bitchPlease");
                StartCoroutine(Idle());
            }
        }
        else if (_state == GhostState.RESET)
        {
            Debug.Log("C");
            if (Vector3.Distance(transform.position, _resetPosition) < 0.1f)
            {
                Debug.Log("XDXDXD");
                _state = GhostState.PATROL;
            }
        }
    }

    void OnTriggerStay(Collider collider)
    {
        _self.SetDestination(collider.transform.position);
        _state = GhostState.CHASE;
        StopCoroutine(Idle());
        Debug.Log("TRIGGERSTY");
    }

    void OnTriggerExit(Collider collider)
    {
        Debug.Log("EXIT");
        StartCoroutine(Idle());
    }

    IEnumerator Idle()
    {
        Debug.Log("IDLE");
        _state = GhostState.THINK;
        yield return new WaitForSeconds(3f);
        _state = GhostState.RESET;
        Debug.Log(gameObject.name + " " + _resetPosition);
        _self.SetDestination(_resetPosition);
    }

    void MoveTowards(Vector3 target)
    {
        Debug.Log("MOVETO");
        _state = GhostState.CHASE;
        _target = new Vector3(target.x, transform.position.y, target.z);
        _self.SetDestination(_target);
    }

    void OnDestroy()
    {
        GargoyleController._alert -= MoveTowards;
    }
}
