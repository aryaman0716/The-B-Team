using UnityEngine;
public class Pickup : MonoBehaviour
{
    bool isHolding = false;

    [SerializeField] float throwForce = 500f;
    [SerializeField] float maxDistance = 3f;

    [SerializeField] PlacementHighlight ph;
    float distance;
    PropHolder propHolder;
    Rigidbody rb;

    Vector3 objectPos;

    private EquipmentController equipmentController;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        propHolder = PropHolder.Instance;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            equipmentController = player.GetComponent<EquipmentController>();
    }
    void Update()
    {
        if (isHolding)
        {
            Hold();
        }
    }
    public bool IsHolding => isHolding;  // making this public so that other scripts can check if this object is currently being held

    private void OnMouseDown()
    {
        if (GetComponent<Pickup>().enabled == false) { return; }//Disabling the script doesn't stop non monobehaviour functions from running, such as unity input system
        // pick up the object
        if (propHolder != null)
        {
            distance = Vector3.Distance(this.transform.position, propHolder.transform.position);
            if (distance <= maxDistance)
            {
                isHolding = true;

                if (CursorManager.Instance != null)
                {
                    CursorManager.Instance.SetCloseHand();
                }
                rb.useGravity = false;
                rb.detectCollisions = true;

                this.transform.SetParent(propHolder.transform);

                // Disable equipping while holding this object and hide equipped tool
                if (equipmentController != null)
                {
                    equipmentController.SetCanEquip(false);
                    equipmentController.SetHolding(true);
                }

                var blender = GetComponent<BlenderPuree>();
                if (blender != null)
                    blender.SetHeld(true);
            }
        }
        else
        {
            Debug.Log("PropHolder instance not found in scene.");
        }
    }
    private void OnMouseUp()
    {
        Drop();
    }
    private void OnMouseEnter()
    {
        if (!isHolding && CursorManager.Instance != null)
        {
            CursorManager.Instance.SetOpenHand();
        }
    }
    private void OnMouseExit()
    {
        if (!isHolding && CursorManager.Instance != null)
        {
            CursorManager.Instance.SetNormal();
        }
    }
    private void Hold()
    {
        

        distance = Vector3.Distance(this.transform.position, propHolder.transform.position);

        if (distance >= maxDistance)
        {
            Drop();
            return;
        }

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        //if (Input.GetMouseButton(1))
        //{
        //    // throw the object
        //    rb.AddForce(propHolder.transform.forward * throwForce);
        //    Drop();
        //}
        if (Input.GetMouseButton(1))
        {
            
            ManagerKey isManagerKey = GetComponent<ManagerKey>();

            
            if (isManagerKey == null)
            {
                rb.AddForce(propHolder.transform.forward * throwForce);
                Drop();
                Debug.Log("Object thrown!");
            }
            else
            {
                Debug.Log("Manager Key detected: Throwing disabled.");
            }
        }
    }
    public void Drop()
    {
        if (isHolding)
        {
            isHolding = false;
            objectPos = this.transform.position;

            this.transform.position = objectPos;
            this.transform.SetParent(null);
            rb.useGravity = true;

            // Re-enable equipping and restore equipped tool visibility when dropped
            if (equipmentController != null)
            {
                equipmentController.SetCanEquip(true);
                equipmentController.SetHolding(false);
            }
            var blender = GetComponent<BlenderPuree>();
            if (blender != null)
                blender.SetHeld(false);
        }
        if (CursorManager.Instance != null)
        {
            CursorManager.Instance.SetNormal();
        }
        if (ph != null & ph.InPlace)//If object is in range of placement area when dropped, set position
        {
            
            Transform t = ph.GetPlacementTransform();
            transform.position = t.position;
            transform.rotation = t.rotation;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            GetComponent<Pickup>().enabled = false;
            ph.isPlaced = true;
            return;

        }
    }
}