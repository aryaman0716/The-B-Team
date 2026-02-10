using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public enum ObjectType { General, Knife, Vent }
    public ObjectType type; 

    public bool canBePickedUp = true;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnPickUp(Transform holdPoint)
    {
        if (rb) rb.isKinematic = true; 
        transform.SetParent(holdPoint); 
        transform.localPosition = new Vector3(0.5f, -0.5f, 1f); 
        transform.localRotation = Quaternion.identity;
    }

    public void OnDrop()
    {
        if (rb) rb.isKinematic = false; 
        transform.SetParent(null); 
    }
}