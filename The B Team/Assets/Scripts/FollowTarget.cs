using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        if (target == null) { return; }
        transform.position = target.position;
    }
}
