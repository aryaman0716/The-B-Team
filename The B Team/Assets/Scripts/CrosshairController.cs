using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
public class CrosshairController : MonoBehaviour
{
    private Image cursorImage;
    [SerializeField]private GameObject seperatingLine;
    [SerializeField] private Sprite[] cursorSprites;
    [SerializeField] private Vector2[] cursorSizes;
    [SerializeField] private Sprite[] popupIcons;
    public static int handshape = 0;
    public static bool anyFocus = false;
    private RectTransform rectTransform;
    private Canvas canvas;
    [SerializeField] private Vector2 pausedOffset;
    [SerializeField] private Vector2 normalOffset;

    private Image popupImage;
    private TMP_Text popupText;

    public enum cursor_img : int
    {
        none,
        open_hand,
        point,
        grab,
        disabled,
        knife,
        spatula,
        tomato,
        flour

    }

    public enum ui_img : int
    {
        none,
        mouse_left,
        mouse_right
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cursorImage = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        InitialisePopupUI();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleUI();
        if (SceneManager.GetActiveScene().name == "Cutscene" || SceneManager.GetActiveScene().name == "CutsceneEnd")
        {
            return;
        }
        if (cursorSprites[0] != null && cursorImage.sprite != cursorSprites[handshape])
        {
            cursorImage.sprite = cursorSprites[handshape];
            rectTransform.sizeDelta = cursorSizes[handshape];
        }
        bool overUI = IsPointerOverUI();


        
        if (anyFocus || UIController.Paused || SceneManager.GetActiveScene().name != "Room1Blockout")
        {
            FollowMouse();
        }
        else
        {
            CenterCrosshair();
        }

        

        KeypadButtonScript.mousingB = false;
        MicrowaveController.mousingM = false;
        SinkInteractable.mousingS = false;
        Pickup.mousing = false;

    }

    void FollowMouse()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition,
            canvas.worldCamera,
            out pos
        );
        Vector2 offset = UIController.Paused ? pausedOffset : normalOffset;
        rectTransform.anchoredPosition = pos + (offset / canvas.scaleFactor);
        //offset for hands in ui

        //should make the crosshair follow the mouse if its ina focus state like a keypad
    }

    void CenterCrosshair()
    {
        rectTransform.anchoredPosition = Vector2.zero;
    }
    bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    void HandleUI()
    {
        if((UIController.Paused || SceneManager.GetActiveScene().name != "Room1Blockout"))
        {
            if (IsPointerOverUI())
            {
                EnablePopupUI("", (int)cursor_img.point, (int)ui_img.none);
                return;
            }
            DisablePopupUI();
            return;
        }
        if (Room4_Microwave.mousing)
        {
            var obj = GameObject.Find("Room4_Microwave").GetComponent<Room4_Microwave>();
            
            if (obj != null && obj.Openable && !obj.DoorOpened)
            {
                EnablePopupUI("Open", (int)cursor_img.grab, (int)ui_img.mouse_left);
                return;
            }
            else if (obj != null && obj.Openable && obj.DoorOpened)
            {
                EnablePopupUI("Close", (int)cursor_img.open_hand, (int)ui_img.mouse_left);
                return;
            }

        }
        if (Pickup.carrying)
        {
            EnablePopupUI("Throw", (int)cursor_img.grab, (int)ui_img.mouse_right);
            return;
        }
        if (Pickup.mousing)
        {
            EnablePopupUI("Grab", (int)cursor_img.open_hand, (int)ui_img.mouse_left);
            return;
        }
        if (GeneralDoor.currentDoor != null)
        {
            
            if (GameObject.Find("Room1ExitDoor").GetComponent<GeneralDoor>() == GeneralDoor.currentDoor && GeneralDoor.currentDoor.locked)
            {
                EnablePopupUI("Jammed...", (int)cursor_img.disabled, (int)ui_img.none, 400f, 10f);
                return;
            }
            if (GeneralDoor.currentDoor.locked == true)
            {
                EnablePopupUI("It's locked...", (int)cursor_img.disabled, (int)ui_img.none, 400f, 10f);
                return;
            }
            if (GeneralDoor.currentDoor.opened)
            {
                EnablePopupUI("Close", (int)cursor_img.open_hand, (int)ui_img.mouse_left);
                return;
            }
            else
            {
                EnablePopupUI("Open", (int)cursor_img.open_hand, (int)ui_img.mouse_left);
                return;
            }
        }
        if (SinkInteractable.mousingS)
        {
            var obj = GameObject.Find("Sink_Base").GetComponent<SinkInteractable>();
            if (obj.Filled)
            {
                var sinkMix = FindFirstObjectByType<SinkMixSystem>();
                if (sinkMix.FlourAdded)
                {
                    if (EquipmentController.publicIndex != 4)
                    {
                        EnablePopupUI("Knead", (int)cursor_img.disabled, (int)ui_img.none, 400f, 10f);
                        return;
                    }
                    EnablePopupUI("Knead", (int)cursor_img.open_hand, (int)ui_img.mouse_left);
                    return;
                }
                if(EquipmentController.publicIndex != 3)
                {
                    EnablePopupUI("Add", (int)cursor_img.disabled, (int)ui_img.none, 400f, 10f);
                    return;
                }
                EnablePopupUI("Add", (int)cursor_img.flour, (int)ui_img.mouse_left);
                return;

                DisablePopupUI();
                return;
            }
            if (obj.IsOn)
            {
                EnablePopupUI("Turn Off", (int)cursor_img.point, (int)ui_img.mouse_left);
                return;
            }
            else
            {
                EnablePopupUI("Turn On", (int)cursor_img.point, (int)ui_img.mouse_left);
                return;
            }
        }
        if (MicrowaveController.mousingM)
        {
            var obj = GameObject.Find("Microwave").GetComponent<MicrowaveController>();
            if (obj == null || obj.enabled == false || obj.KeyCooked) { return; }
            
            if (obj.open)
            {
                EnablePopupUI("Close", (int)cursor_img.open_hand, (int)ui_img.mouse_left);
                return;
            }
            else
            {
                EnablePopupUI("Open", (int)cursor_img.grab, (int)ui_img.mouse_left);
                return;
            }
        }
        if (KeypadButtonScript.mousingB)
        {
            EnablePopupUI("Push", (int)cursor_img.point, (int)ui_img.mouse_left);
            return;
        }
        if (ScrewInteractable.mousing)
        {
            if (EquipmentController.publicIndex != 0)
            {
                EnablePopupUI("Unscrew", (int)cursor_img.disabled, (int)ui_img.none, 400f, 10f);
                return;
            }
            EnablePopupUI("Unscrew", (int)cursor_img.knife, (int)ui_img.mouse_left);
            return;
        }
        if (VentFlourTarget.mousing)
        {
            if (EquipmentController.publicIndex != 3)
            {
                EnablePopupUI("Splash", (int)cursor_img.disabled, (int)ui_img.none, 400f, 10f);
                return;
            }
            EnablePopupUI("Splash", (int)cursor_img.flour, (int)ui_img.mouse_left);
            return;
        }
        if (cameraWire.mousing)
        {
            if (EquipmentController.publicIndex != 0)
            {
                EnablePopupUI("<s>Slash</s>", (int)cursor_img.disabled, (int)ui_img.none, 400f, 10f);
                return;
            }
            EnablePopupUI("Slash", (int)cursor_img.knife, (int)ui_img.mouse_left);
            return;
        }
        if (ManagerRoomButton.mouseOver)
        {
            EnablePopupUI("Unlock Factory Door", (int)cursor_img.point, (int)ui_img.mouse_left);
            return;
        }
        if (FridgeDoor.mousing)
        {
            if(EquipmentController.publicIndex != 1)
            {
                EnablePopupUI("Pry Open", (int)cursor_img.disabled, (int)ui_img.none, 400f, 10f);
                return;
            }
            EnablePopupUI("Pry Open", (int)cursor_img.spatula, (int)ui_img.mouse_left);
            return;
        }

        DisablePopupUI();
        
    }

    void InitialisePopupUI()
    {
        if(SceneManager.GetActiveScene().name != "Room1Blockout") { return; }
        popupText = GameObject.Find("popupText").GetComponent<TMP_Text>();
        popupImage = GameObject.Find("popupImage").GetComponent<Image>();   
    }

    void EnablePopupUI(string text, int cursorIndex, int imgIndex)
    {
        if (popupText != null)
        {
            popupText.text = text;
            popupImage.sprite = popupIcons[imgIndex];
            popupText.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(520f, 10f);
        }
        handshape = cursorIndex;
        //seperatingLine.SetActive(true);
    }
    void EnablePopupUI(string text, int cursorIndex, int imgIndex, float posX, float posY)
    {
        if (popupText != null) 
        {

            popupText.text = text;
            popupImage.sprite = popupIcons[imgIndex];
            popupText.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX, posY);
        }
        handshape = cursorIndex;
        //seperatingLine.SetActive(true);
        
    }

    void DisablePopupUI()
    {

        if (popupText != null) 
        { 
            popupText.text = ""; 
            popupImage.sprite = popupIcons[(int)ui_img.none]; 
            popupText.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(520f, 10f); 
        }
        handshape = (int)cursor_img.none;
        //seperatingLine.SetActive(false);
    }




}
