using UnityEngine;
using System.Collections.Generic;

public class ItemInteraction : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float interactDistance = 4f;
    [SerializeField] private int maxInventorySize = 5;

    [Header("References")]
    [SerializeField] private Transform propsHolder;
    [SerializeField] private Camera playerCamera;

    private List<GameObject> inventory = new List<GameObject>();
    private GameObject currentHoldingItem;

    void Start()
    {
        // item in hand
        if (propsHolder.childCount > 0)
        {
            currentHoldingItem = propsHolder.GetChild(0).gameObject;
            SetupItemInHand(currentHoldingItem);
            currentHoldingItem.SetActive(false); // hide if click right ->show last item
        }
    }

    void Update()
    {
        // 1. click left -> get item
        if (Input.GetMouseButtonDown(0))
        {
            TryPickUpItem();
        }

        // 2. hold right -> show item
        if (currentHoldingItem != null)
        {
            bool isHolding = Input.GetMouseButton(1);

            
            if (currentHoldingItem.activeSelf != isHolding)
            {
                currentHoldingItem.SetActive(isHolding);
            }
        }
    }

    void TryPickUpItem()
    {
        RaycastHit hit;
        // Raycast 
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactDistance))
        {
            //  Tag obj
            if (hit.collider.CompareTag("Interactable"))
            {
                HandleInventoryLogic(hit.collider.gameObject);
            }
        }
    }

    void HandleInventoryLogic(GameObject newItem)
    {
        // 5 items full
        if (inventory.Count >= maxInventorySize)
        {
            DropCurrentItem();
        }
        AddToInventory(newItem);
    }

    void AddToInventory(GameObject item)
    {
        inventory.Add(item);
        SetupItemInHand(item);
        item.SetActive(false); // hide bag
        currentHoldingItem = item;
    }

    void DropCurrentItem()
    {
        if (currentHoldingItem != null)
        {
            inventory.Remove(currentHoldingItem);
            currentHoldingItem.transform.SetParent(null);

            
            Rigidbody rb = currentHoldingItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }

           
            currentHoldingItem.transform.position = transform.position + transform.forward * 1.5f;
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
        }

        
        item.transform.localPosition = new Vector3(0, 0, 0.5f);
        item.transform.localRotation = Quaternion.identity;
    }
}