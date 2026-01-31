using UnityEngine;
using System.Collections.Generic;

public class ItemInteraction : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private float interactDistance = 4f;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform propsHolder;

    [Header("References")]
    [SerializeField] private PlayerInventory inventory; 

    private WorldItem heldWorldItem; // we make a reference to the WorldItem script

    void Update()
    {
        HandleRightClick();
        HandleLeftClick();
    }

    // Right click to grab or drop an item
    void HandleRightClick()
    {
        if (!Input.GetMouseButtonDown(1)) return;
        
        if (heldWorldItem == null)
        {
            TryGrabItem();
        }
        else
        {
            DropHeldItem();
        }
    }

    // Left click to store the held item in inventory
    void HandleLeftClick()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        
        if (heldWorldItem == null) return;

        if (!inventory.HasFreeSlot())
        {
            Debug.Log("Inventory Full!");
            return;
        }

        inventory.AddItem(heldWorldItem.itemData);
        Destroy(heldWorldItem.gameObject);
        heldWorldItem = null; 

        Debug.Log("Item added to inventory");
    }

    void TryGrabItem()
    {
        // Raycast to find an item in front of the player
        if (!Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, interactDistance)) return;
        
        WorldItem worldItem = hit.collider.GetComponent<WorldItem>(); 
        if (worldItem == null) return;
        
        heldWorldItem = worldItem;  // reference to the WorldItem script

        worldItem.transform.SetParent(propsHolder);
        worldItem.transform.localPosition = Vector3.forward * 0.5f; // position the item in front of the player
        worldItem.transform.localRotation = Quaternion.identity;   // reset rotation

        if (worldItem.TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }   
    }

    // Drop the currently held item
    void DropHeldItem()
    {
        heldWorldItem.transform.SetParent(null);
        heldWorldItem.transform.position = playerCamera.transform.position + playerCamera.transform.forward * 1.5f;
        if (heldWorldItem.TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }   
        heldWorldItem = null;
    }
}