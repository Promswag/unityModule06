using UnityEngine;

public class Caught : MonoBehaviour
{
    public void ResetState()
    {
        GameManager.Instance.ResetFadeState();
    }
}
