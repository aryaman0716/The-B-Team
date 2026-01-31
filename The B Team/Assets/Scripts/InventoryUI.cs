using UnityEngine;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private GameObject slotPrefab;

    [Header("Slot Parents")]
    [SerializeField] private Transform toolSlotsParent;
    [SerializeField] private Transform foodSlotsParent;

    private Image[] slotIcons;

    void Start()
    {
        slotIcons = new Image[inventory.slots.Count];

        for (int i = 0; i < inventory.slots.Count; i++)
        {
            Transform parent = i < 2 ? toolSlotsParent : foodSlotsParent;  // First 2 slots are tools, rest are food

            GameObject slotGO = Instantiate(slotPrefab, parent);  // Create slot UI
            slotIcons[i] = slotGO.transform.GetChild(0).GetComponent<Image>();  // Get the icon Image component
        }
    }
    void Update()
    {
        // Update UI icons based on inventory slots
        if (slotIcons == null || slotIcons.Length == 0) return;

        for (int i = 0; i < inventory.slots.Count; i++)
        {
            
            if (i >= slotIcons.Length) break;

            if (inventory.slots[i].IsEmpty)
            {
                slotIcons[i].enabled = false;
            }
            else
            {
                slotIcons[i].enabled = true;
                slotIcons[i].sprite = inventory.slots[i].item.icon;
            }
        }
    }
}
