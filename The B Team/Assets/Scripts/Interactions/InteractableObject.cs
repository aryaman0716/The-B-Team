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
        if (rb != null)
        {
            rb.useGravity = false;      
            rb.isKinematic = true;      
            rb.linearVelocity = Vector3.zero; 
        }
        transform.SetParent(holdPoint); 
        transform.localPosition = new Vector3(0.5f, -0.5f, 1f); 
        transform.localRotation = Quaternion.identity;
    }

    public void OnDrop()
    {
        transform.SetParent(null);
        if (rb != null)
        {
            rb.useGravity = true;       
            rb.isKinematic = false;     
        }
    }

    
    public void InteractWith(InteractableObject itemInHand)
    {
        if (type == ObjectType.Vent && itemInHand.type == ObjectType.Knife)
        {
            
            Debug.Log("Vent Opened!");
            gameObject.SetActive(false);
        }
    }
}