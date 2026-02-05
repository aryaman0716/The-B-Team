using UnityEngine;
using UnityEngine.UI;
public class HotbarUI : MonoBehaviour
{
    [Header("References")]
    public EquipmentController equipment;
    public Transform leftPanel;
    public Transform rightPanel;
    public GameObject slotPrefab;

    private Image[] slotBackgrounds;

    void Start()
    {
        // Create 4 slots (2 on each panel)
        int slotCount = 4;
        slotBackgrounds = new Image[slotCount];

        for (int i = 0; i < slotCount; i++)
        {
            Transform parent = i < 2 ? leftPanel : rightPanel;
            GameObject slot = Instantiate(slotPrefab, parent);
            slotBackgrounds[i] = slot.GetComponent<Image>();
            Image icon = slot.transform.GetChild(0).GetComponent<Image>();
            icon.enabled = true;
            icon.sprite = equipment.GetToolIcon(i);
        }
    }
    void Update()
    {
        int selected = equipment.GetCurrentIndex();
        for (int i = 0; i < slotBackgrounds.Length; i++)
        {
            slotBackgrounds[i].color = (i == selected) ? Color.yellow : Color.white;
        }
    }
}

