using UnityEngine;
public class Pickup : MonoBehaviour
{
    bool isHolding = false;

    [SerializeField] float throwForce = 500f;
    [SerializeField] float maxDistance = 3f;

    float distance;
    PropHolder propHolder;
    Rigidbody rb;

    Vector3 objectPos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        propHolder = PropHolder.Instance;
    }
    void Update()
    {
        if (isHolding)
        {
            Hold();
        }
    }
    private void OnMouseDown()
    {
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
            }
        }
        else
        {
            Debug.Log("PropHolder instance not found in scene.");
        }
    }
    private void OnMouseUp()
    {
        // drop the object
        Drop();
    }
    private void OnMouseEnter()
    {
        Debug.Log("Mouse Hovering on: " + gameObject.name);
        float currentDist = Vector3.Distance(transform.position, PropHolder.Instance.transform.position);

        if (!isHolding && currentDist <= maxDistance && CursorManager.Instance != null)
        {
            CursorManager.Instance.SetOpenHand();
        }
    }
    private void OnMouseExit()
    {
        if (!isHolding && CursorManager.Instance != null)
        {
            CursorManager.Instance.SetDefault();
        }
    }
    private void Hold()
    {
        distance = Vector3.Distance(this.transform.position, propHolder.transform.position);

        if (distance >= maxDistance)
        {
            Drop();
        }

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if (Input.GetMouseButton(1))
        {
            // throw the object
            rb.AddForce(propHolder.transform.forward * throwForce);
            Drop();
        }
    }
    private void Drop()
    {
        if (isHolding)
        {
            isHolding = false;
            objectPos = this.transform.position;
            this.transform.position = objectPos;
            this.transform.SetParent(null);
            rb.useGravity = true;
        }
        if (CursorManager.Instance != null)
        {
            CursorManager.Instance.SetDefault();
        }
    }
}
