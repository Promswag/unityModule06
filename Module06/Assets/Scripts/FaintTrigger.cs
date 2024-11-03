using UnityEngine;

public class FaintTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Ghost")
        {
            GameManager.Instance.Caught();
        }
    }
}
