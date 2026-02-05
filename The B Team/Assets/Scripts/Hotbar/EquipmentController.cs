using UnityEngine;
public class EquipmentController : MonoBehaviour
{
    [Header("Tools")]
    [SerializeField] private ToolData[] tools;

    [Header("References")]
    [SerializeField] private Transform propsHolder;

    private int currentIndex = 0;
    private GameObject currentToolObject;

    void Start()
    {
        EquipTool(0);
    }
    void Update()
    {
        HandleScrollInput();
        HandleKeyInput();
    }

    void HandleScrollInput()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
            CycleTool(1);  // Scroll up to cycle forward
        if (scroll < 0f)
            CycleTool(-1);  // Scroll down to cycle backward
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
    }
    void CycleTool(int direction)
    {
        currentIndex += direction;  // Move index based on scroll direction

        // Wrap around the index if it goes out of bounds
        if (currentIndex < 0)
            currentIndex = tools.Length - 1;  
        if(currentIndex >= tools.Length)
            currentIndex = 0;  

        EquipTool(currentIndex); 
    }
    void EquipTool(int index)
    {
        if (index < 0 || index >= tools.Length)
            return;  
        currentIndex = index;  

        // Destroy the currently equipped tool if it exists
        if (currentToolObject != null)
            Destroy(currentToolObject);

        // Instantiate the new tool and parent it to the props holder
        currentToolObject = Instantiate(tools[index].toolPrefab, propsHolder);
        currentToolObject.transform.localPosition = new Vector3(0.5f, -0.5f, 1f);  // Reset position
        currentToolObject.transform.localRotation = Quaternion.identity;  // Reset rotation
    }
    public Sprite GetToolIcon(int index)
    {
        return tools[index].toolIcon;
    }
    public int GetCurrentIndex()
    {
        return currentIndex;
    }
}
