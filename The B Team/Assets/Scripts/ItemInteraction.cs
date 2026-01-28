using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float interactDistance = 4f;
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    [Header("References")]
    [SerializeField] private Transform propsHolder;
    [SerializeField] private Camera playerCamera;

    void Start()
    {
        
        if (propsHolder.childCount > 0)
        {
            SetupItemInHand(propsHolder.GetChild(0).gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactDistance))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                SwapItems(hit.collider.gameObject);
            }
        }
    }

    void SwapItems(GameObject newItem)
    {
        
        if (propsHolder.childCount > 0)
        {
            GameObject oldItem = propsHolder.GetChild(0).gameObject;
            oldItem.transform.SetParent(null);

            Rigidbody oldRb = oldItem.GetComponent<Rigidbody>();
            if (oldRb != null)
            {
                oldRb.isKinematic = false; 
                oldRb.useGravity = true;
            }

            
            oldItem.transform.position = transform.position + transform.forward * 1.2f + Vector3.up * 0.5f;
        }

        
        SetupItemInHand(newItem);
    }

    void SetupItemInHand(GameObject item)
    {
        item.transform.SetParent(propsHolder);

       
        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        
        item.transform.localPosition = new Vector3(0, 0, 0.5f);
        item.transform.localRotation = Quaternion.identity;
    }
}