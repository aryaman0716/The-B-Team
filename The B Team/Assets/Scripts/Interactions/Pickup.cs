using UnityEngine;
public class Pickup : MonoBehaviour
{
    bool isHolding = false;

    private static GameObject heldObject;
    public GameObject HeldObject => heldObject;

    [SerializeField] float throwForce = 500f;
    [SerializeField] float maxDistance = 3f;

    float distance;
    PropHolder propHolder;
    Rigidbody rb;

    Vector3 objectPos;
    Vector3 holdOffset;

    private EquipmentController equipmentController;
    public static bool carrying = false;
    public static bool mousing = false;


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
            if (Input.GetMouseButtonUp(0))
            {
                Drop();
            }
        }
    }
    public bool IsHolding => isHolding;  // making this public so that other scripts can check if this object is currently being held

    public void OnMouseOver()
    {
        if (EquipmentController.publicIndex < 4)
        {
            return;
        }
        if (propHolder != null)
        {
            distance = Vector3.Distance(this.transform.position, propHolder.transform.position);
            if (distance <= maxDistance)
            {
                mousing = true;
            }
        }

    }


    private void OnMouseDown()
    {
        if (EquipmentController.publicIndex < 4)
        {
            return;
        }



        if (!enabled) { return; }//Disabling the script doesn't stop non monobehaviour functions from running, such as unity input system
        // pick up the object
        if (propHolder != null)
        {
            distance = Vector3.Distance(this.transform.position, propHolder.transform.position);
            if (distance <= maxDistance)
            {
                isHolding = true;

                rb.useGravity = false;
                rb.isKinematic = true;
                rb.detectCollisions = false;
                carrying = true;

                this.transform.SetParent(propHolder.transform);
                holdOffset = transform.localPosition;

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
            else { carrying = false; }
        }
        else
        {
            Debug.Log("PropHolder instance not found in scene.");
        }
    }

     
    //private void OnMouseUp()
    //{
    //    Drop();
    //    carrying = false;
    //}
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

        transform.localPosition = holdOffset;
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
                rb.isKinematic = false;
                rb.detectCollisions = true;
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
        carrying = false;
        if (isHolding)
        {
            isHolding = false;
            objectPos = this.transform.position;

            this.transform.position = objectPos;
            this.transform.SetParent(null);
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.detectCollisions = true;

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
    }
    private void OnDisable()
    {
        carrying = false;
        mousing = false;
    }

}