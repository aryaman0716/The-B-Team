using UnityEngine;
using System.Collections.Generic;

public class ItemInteraction : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float interactDistance = 4f;
    //[SerializeField] private int maxInventorySize = 5;
    [SerializeField] private float throwForce = 600f;

    [Header("References")]
    [SerializeField] private Transform propsHolder;
    [SerializeField] private Camera playerCamera;


    //private List<GameObject> inventory = new List<GameObject>();
    private GameObject currentHoldingItem;

    void Start()
    {
        // item in hand
        if (propsHolder.childCount > 0)
        {
            currentHoldingItem = propsHolder.GetChild(0).gameObject;
            SetupItemInHand(currentHoldingItem);
            //currentHoldingItem.SetActive(false); // hide if click right ->show last item
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryPickUpItem();
        }

        if (currentHoldingItem != null && Input.GetKeyDown(KeyCode.Q)) 
        {
            DropCurrentItem();
        }
    }

    void TryPickUpItem()
    {
        // (Viewport Center)
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                HandleInventoryLogic(hit.collider.gameObject);
            }
        }
    }

    void HandleInventoryLogic(GameObject newItem)
    {
        
        if (currentHoldingItem != null)
        {
            DropCurrentItem();
        }

        
        SetupItemInHand(newItem);

        
        currentHoldingItem = newItem;

        
    }

    void AddToInventory(GameObject item)
    {
        //inventory.Add(item);
        SetupItemInHand(item);
        item.SetActive(false); // hide bag
        currentHoldingItem = item;
    }

    void DropCurrentItem()
    {
        if (currentHoldingItem != null)
        {
            

            
            currentHoldingItem.transform.SetParent(null);

            
            currentHoldingItem.transform.position = playerCamera.transform.position + playerCamera.transform.forward * 1.0f;

            Rigidbody rb = currentHoldingItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;

               
                rb.AddForce(playerCamera.transform.forward * 600f);
            }

            currentHoldingItem.SetActive(true);
            currentHoldingItem = null;
        }
    }

    void SetupItemInHand(GameObject item)
    {
        item.transform.SetParent(propsHolder);

        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
            
            rb.linearVelocity = Vector3.zero; 
            rb.angularVelocity = Vector3.zero;
        }

        item.transform.localPosition = new Vector3(0.5f, -0.4f, 1.0f);
        item.transform.localRotation = Quaternion.Euler(0, 90, 0);
    }


}