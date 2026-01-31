using UnityEngine;
public enum ItemType
{
    Tool, 
    Food
}

// ScriptableObject to hold item data for inventory items
[CreateAssetMenu(menuName = "Inventory/Item")] 
public class ItemData : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public Sprite icon;
    public GameObject worldPrefab;
}
