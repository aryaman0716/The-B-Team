using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 3f;
    public Transform holdPoint; 

    private InteractableObject heldObject;

    void Update()
    {
        HandleLeftClick();
        HandleRightClick();
    }

    //(Hold Left Mouse
    void HandleLeftClick()
    {
        if (Input.GetMouseButtonDown(0))
        { 
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
            {
                InteractableObject obj = hit.collider.GetComponent<InteractableObject>();
                if (obj != null && obj.canBePickedUp)
                {
                    heldObject = obj;
                    heldObject.OnPickUp(holdPoint);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        { 
            if (heldObject != null)
            {
                heldObject.OnDrop();
                heldObject = null;
            }
        }
    }

    // Hold Right Mouse
    void HandleRightClick()
    {
        if (Input.GetMouseButton(1))
        { 
            if (heldObject != null && heldObject.type == InteractableObject.ObjectType.Knife)
            {

                
                Ray ray = new Ray(transform.position, transform.forward);
                if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
                {
                    InteractableObject target = hit.collider.GetComponent<InteractableObject>();

                    if (target != null && target.type == InteractableObject.ObjectType.Vent)
                    {
                        Debug.Log("Doing...");
                        
                    }
                }
            }
        }
    }
}