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

    private EquipmentController equipmentController;
    public static bool carrying = false;
    public static bool mousing = false;

    private int holdLayer;
    private Rigidbody heldObjRb;
    private GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        propHolder = PropHolder.Instance;

        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            equipmentController = player.GetComponent<EquipmentController>();

        holdLayer = LayerMask.NameToLayer("holdLayer");
    }
    void Update()
    {
        if (isHolding)
        {
            Hold();
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
                heldObject = gameObject;
                heldObjRb = rb;
                rb.useGravity = false;
                rb.isKinematic = true;

                transform.SetParent(propHolder.transform);

                gameObject.layer = holdLayer;

                Collider objCol = GetComponent<Collider>();
                Collider playerCol = player.GetComponent<Collider>();
                if (objCol && playerCol)
                {
                    Physics.IgnoreCollision(objCol, playerCol, true);
                }
                carrying = true;

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


    private void OnMouseUp()
    {
        Drop();
        carrying = false;
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
        if (!isHolding) return;

        float holdDistance = 2.0f;
        transform.position = propHolder.transform.position + propHolder.transform.forward * holdDistance + Vector3.down * 0.2f;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        distance = Vector3.Distance(transform.position, propHolder.transform.position);
        if (distance >= maxDistance)
        {
            Drop();
            return;
        }

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
    void StopClipping()
    {
        // Check if the object is clipping into the camera and adjust its position if necessary
        float clipRange = Vector3.Distance(propHolder.transform.position, Camera.main.transform.position);
        RaycastHit[] hits = Physics.RaycastAll(Camera.main.transform.position, Camera.main.transform.forward, clipRange);
        if (hits.Length > 1)
        {
            transform.position = Camera.main.transform.position + new Vector3(0, -0.5f, 0);  // moving the object downwards to prevent clipping into the camera
        }
    }
    void ThrowObject()
    {
        Collider objCol = GetComponent<Collider>();
        Collider playerCol = player.GetComponent<Collider>();

        if (objCol && playerCol)
            Physics.IgnoreCollision(objCol, playerCol, false);

        gameObject.layer = 0;

        rb.isKinematic = false;
        rb.useGravity = true;

        transform.SetParent(null);

        rb.AddForce(Camera.main.transform.forward * throwForce);

        heldObject = null;
        isHolding = false;
    }
    public void Drop()
    {
        carrying = false;
        if (isHolding)
        {
            isHolding = false;
            StopClipping();
            Collider objCol = GetComponent<Collider>();
            Collider playerCol = player.GetComponent<Collider>();

            if (objCol && playerCol)
            {
                Physics.IgnoreCollision(objCol, playerCol, false);
            }

            gameObject.layer = 0;
            rb.isKinematic = false;
            rb.useGravity = true;
            transform.SetParent(null);
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // Re-enable equipping and restore equipped tool visibility when dropped
            if (equipmentController != null)
            {
                equipmentController.SetCanEquip(true);
                equipmentController.SetHolding(false);
            }
            var blender = GetComponent<BlenderPuree>();
            if (blender != null)
                blender.SetHeld(false);

            heldObject = null;
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