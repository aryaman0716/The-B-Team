//using UnityEditor;
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

    void Start() => EquipTool(0);
    void Update()
    {
        HandleScrollInput();
        HandleKeyInput();
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
        if (currentIndex >=0 && currentIndex < tools.Length)
        {
            
                tools[currentIndex].UseTool(propsHolder);
            
        }
    }
    void CycleTool(int direction)
    {
        currentIndex += direction;  // Move index based on scroll direction
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

        // Destroy the currently equipped tool if it exists
        if (currentToolObject != null)
            Destroy(currentToolObject);

        // Instantiate the new tool and parent it to the props holder
        currentToolObject = Instantiate(tools[index].toolPrefab, propsHolder);
        currentToolObject.transform.localPosition = new Vector3(0.5f, -0.35f, 1.0f);  // Reset position
        //currentToolObject.transform.localRotation = Quaternion.identity;  // Reset rotation
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
}
