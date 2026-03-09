using UnityEngine;

public class ConveyorChild : MonoBehaviour
{
    private ConveyerBelt parent;

    void Start()
    {
        parent = GetComponentInParent<ConveyerBelt>();
    }

    private void OnCollisionStay(Collision collision)
    {
        parent.HandleCollision(collision);
    }
}