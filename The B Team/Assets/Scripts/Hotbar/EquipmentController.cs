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
    public static int publicIndex;

    void Start() => UnequipTool();

    public AudioSource toolSoundSource;

    public AudioClip[] knifeSounds;
    public AudioClip[] spatulaSounds;
    public AudioClip[] tomatoSounds;
    public AudioClip[] flourSounds;
    void Update()
    {
        publicIndex =  currentIndex;
        if (canEquip)
        {
            HandleScrollInput();
            HandleKeyInput();
        }

        if (Input.GetMouseButtonDown(0))
        {
            UseCurrentTool();
        }
        if (Input.GetMouseButtonDown(1))
        {
            // validity check for current tool
            if (currentIndex >= 0 && currentIndex < tools.Length && tools[currentIndex] != null)
            {
                //// we check if the current tool is a TomatoTool before trying to call UseBlender
                //var tomato = tools[currentIndex] as TomatoTool;
                //if (tomato != null)
                //{
                //    tomato.UseBlender(propsHolder);
                //}
            }
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
            switch (currentIndex)
            {
                case 0:
                    {
                        toolSoundSource.clip = knifeSounds[Random.Range(0, knifeSounds.Length)];
                        break;
                    }
                case 1:
                    {
                        toolSoundSource.clip = spatulaSounds[Random.Range(0, spatulaSounds.Length)];
                        break;
                    }
                case 2:
                    {
                        toolSoundSource.clip = tomatoSounds[Random.Range(0, tomatoSounds.Length)];
                        break;
                    }
                case 3:
                    {
                        toolSoundSource.clip = flourSounds[Random.Range(0, flourSounds.Length)];
                        break;
                    }
            }
            toolSoundSource.pitch = Random.Range(0.9f, 1.1f);
            toolSoundSource.volume = (0.5f * GlobalSettings.MasterVolume * GlobalSettings.SFXVolume);
            toolSoundSource.Play();
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
    public int TotalTools()
    {
        return tools != null ? tools.Length : 0;  // Return the number of tools defined in the array
    }
}