[System.Serializable]
public class InventorySlot
{
    public ItemData item;
    public bool IsEmpty => item == null; // Check if the slot is empty
}
