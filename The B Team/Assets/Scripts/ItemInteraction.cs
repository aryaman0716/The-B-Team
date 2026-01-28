using UnityEngine;
using UnityEngine.UI; // Image UI
using System.Collections.Generic;

public class ItemInteraction : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float interactDistance = 4f;
    [SerializeField] private int maxInventorySize = 5;

    [Header("References")]
    [SerializeField] private Transform propsHolder;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject inventoryPanel; //  InventoryPanel
    [SerializeField] private Image[] inventorySlots; //  Image Slot 5 

    [SerializeField] private List<GameObject> inventory = new List<GameObject>();
    private GameObject currentHoldingItem;

    void Start()
    {
        // hide UI when start game
        inventoryPanel.SetActive(false);

        if (propsHolder.childCount > 0)
        {
            currentHoldingItem = propsHolder.GetChild(0).gameObject;
            SetupItemInHand(currentHoldingItem);
        }
    }

    void Update()
    {
        // 1. Left Click = 0
        if (Input.GetMouseButtonDown(0))
        {
            TryPickUpItem();
        }

        // 2. Hold Right Click = 1
        if (currentHoldingItem != null)
        {
            bool isHolding = Input.GetMouseButton(1);
            currentHoldingItem.SetActive(isHolding);
            inventoryPanel.SetActive(isHolding); // UI
        }
    }

    void TryPickUpItem()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactDistance))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                HandleInventoryLogic(hit.collider.gameObject);
            }
        }
    }

    void HandleInventoryLogic(GameObject newItem)
    {
        if (inventory.Count >= maxInventorySize)
        {
            DropCurrentItem();
        }
        AddToInventory(newItem);
    }

    void AddToInventory(GameObject item)
    {
        inventory.Add(item);
        UpdateInventoryUI(); // update image in UI
        SetupItemInHand(item);
        item.SetActive(false);
        currentHoldingItem = item;
    }

    void UpdateInventoryUI()
    {
        // clear image
        foreach (Image slot in inventorySlots) { slot.sprite = null; slot.enabled = false; }

        // input image in List
        for (int i = 0; i < inventory.Count; i++)
        {
            
            Sprite itemSprite = inventory[i].GetComponent<Image>()?.sprite;
            if (itemSprite != null)
            {
                inventorySlots[i].sprite = itemSprite;
                inventorySlots[i].enabled = true;
            }
        }
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
            UpdateInventoryUI();
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