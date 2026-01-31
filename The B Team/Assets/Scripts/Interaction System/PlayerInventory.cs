using UnityEngine;
using System.Collections.Generic; 
public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int maxSlots = 5;
    public List<InventorySlot> slots = new();  // List to hold inventory slots
    void Awake()
    {
        // Initialize inventory slots
        for (int i = 0; i < maxSlots; i++)
        {
            slots.Add(new InventorySlot()); 
        }
    }
    public bool HasFreeSlot()
    {
        return slots.Exists(s => s.IsEmpty);  // Check for any empty slots
    }
    public bool AddItem(ItemData item)
    {
        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                slot.item = item;
                return true;  // Item added successfully
            }
        }
        return false;  // No free slot available
    }
    public void RemoveItem(int slotIndex)
    {
        // check if the slot index is valid
        if (slotIndex >= 0 && slotIndex < slots.Count)
        {
            slots[slotIndex].item = null;  // Remove item from the specified slot
        }
    }
}
