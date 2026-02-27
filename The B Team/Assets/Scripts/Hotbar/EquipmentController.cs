using UnityEngine;
public class EquipmentController : MonoBehaviour
{
    [Header("Tools")]
    [SerializeField] private ToolData[] tools;

    [Header("References")]
    [SerializeField] private Transform propsHolder;
    [SerializeField] private Transform playerCamera;

    private int currentIndex = 0;
    private GameObject currentToolObject;
    private bool canEquip = true;
    private bool isHoldingObject = false;

    void Start() => EquipTool(0);
    void Update()
    {
        if (canEquip)
        {
            HandleScrollInput();
            HandleKeyInput();
        }

        if (Input.GetMouseButtonDown(0))
        {
            UseCurrentTool();
        }
    }
    void HandleScrollInput()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll < 0f)
            CycleTool(1);  // Scroll down to select next tool
        if (scroll > 0f)
            CycleTool(-1);  // Scroll up to select previous tool
    }
    void HandleKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            EquipTool(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            EquipTool(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            EquipTool(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            EquipTool(3);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            UnequipTool();
    }
    private void UseCurrentTool()
    {
        if (currentIndex >= 0 && currentIndex < tools.Length)
        {
            tools[currentIndex].UseTool(propsHolder);
        }
    }
    void CycleTool(int direction)
    {
        currentIndex += direction;
        int maxIndex = tools.Length;

        // Wrap around the index if it goes out of bounds
        if (currentIndex < 0)
            currentIndex = maxIndex;
        if (currentIndex > maxIndex)
            currentIndex = 0;

        if (currentIndex == tools.Length)
        {
            UnequipTool();
        }
        else
        {
            EquipTool(currentIndex);
        }
    }
    void EquipTool(int index)
    {
        if (index < 0 || index >= tools.Length)
            return;
        currentIndex = index;

        if (currentToolObject != null)
            Destroy(currentToolObject);

        if (tools[index].toolPrefab != null && propsHolder != null)
        {
            currentToolObject = Instantiate(tools[index].toolPrefab, propsHolder);
            currentToolObject.transform.localPosition = tools[index].holdPosition;  // Set position from ToolData

            currentToolObject.SetActive(!isHoldingObject);
        }
    }
    void UnequipTool()
    {
        currentIndex = tools.Length;  //set to empty slot index

        if (currentToolObject != null)
        {
            Destroy(currentToolObject);
            currentToolObject = null;
        }
    }
    public Sprite GetToolIcon(int index)
    {
        if (index >= 0 && index < tools.Length)
            return tools[index].toolIcon;

        return null;
    }
    public int GetCurrentIndex()
    {
        return currentIndex;
    }
    public Animator GetCurrentToolAnimator()
    {
        if (currentToolObject == null) return null;
        return currentToolObject.GetComponentInChildren<Animator>();
    }

    public void SetCanEquip(bool value)
    {
        canEquip = value;
    }

    public bool CanEquip()
    {
        return canEquip;
    }
    public void SetHolding(bool holding)
    {
        isHoldingObject = holding;

        if (currentToolObject != null)
        {
            currentToolObject.SetActive(!holding);
        }
        if (holding)
            canEquip = false;
    }
}