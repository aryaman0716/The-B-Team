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
        if (cursorImage.sprite != cursorSprites[handshape])
        {
            cursorImage.sprite = cursorSprites[handshape];
            rectTransform.sizeDelta = cursorSizes[handshape];
        }
        bool overUI = IsPointerOverUI();


        
        if (anyFocus || UIController.Paused || SceneManager.GetActiveScene().name == "MainMenu")
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
        if((UIController.Paused || SceneManager.GetActiveScene().name == "MainMenu") && IsPointerOverUI())
        {
            EnablePopupUI("", (int)cursor_img.point, (int)ui_img.none);
            return;
        }
        if (Pickup.carrying)
        {
            EnablePopupUI("Throw", (int)cursor_img.grab, (int)ui_img.mouse_left);
            return;
        }
        if (Pickup.mousing)
        {
            EnablePopupUI("Grab", (int)cursor_img.grab, (int)ui_img.mouse_left);
            return;
        }
        if (GeneralDoor.currentDoor != null)
        {
            
            if (GameObject.Find("Room1ExitDoor").GetComponent<GeneralDoor>() == GeneralDoor.currentDoor && GeneralDoor.currentDoor.locked)
            {
                EnablePopupUI("Jammed...", (int)cursor_img.disabled, (int)ui_img.none, 436f, 13f);
                return;
            }
            if (GeneralDoor.currentDoor.locked == true)
            {
                EnablePopupUI("It's locked...", (int)cursor_img.disabled, (int)ui_img.none, 436f, 13f);
                return;
            }
            if (GeneralDoor.currentDoor.opened)
            {
                EnablePopupUI("Turn On", (int)cursor_img.open_hand, (int)ui_img.mouse_left);
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
            if (obj == null || obj.enabled == false) { return; }
            if (obj.Filled)
            {
                var sinkMix = obj.transform.GetComponent<SinkMixSystem>();
                if(sinkMix != null && sinkMix.FlourAdded)
                {
                    EnablePopupUI("Knead", (int)cursor_img.grab, (int)ui_img.mouse_left);
                    return;
                }

                DisablePopupUI();
                return;
            }
            if (obj.IsOn)
            {
                EnablePopupUI("Turn On", (int)cursor_img.point, (int)ui_img.mouse_left);
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
            EnablePopupUI("Unscrew", (int)cursor_img.point, (int)ui_img.mouse_left);
            return;
        }

        DisablePopupUI();
        
    }

    void InitialisePopupUI()
    {
        popupText = GameObject.Find("popupText").GetComponent<TMP_Text>();
        popupImage = GameObject.Find("popupImage").GetComponent<Image>();   
    }

    void EnablePopupUI(string text, int cursorIndex, int imgIndex)
    {
        popupText.text = text;
        popupImage.sprite = popupIcons[imgIndex];
        handshape = cursorIndex;
        seperatingLine.SetActive(true);

    }
    void EnablePopupUI(string text, int cursorIndex, int imgIndex, float posX, float posY)
    {
        popupText.text = text;
        popupImage.sprite = popupIcons[imgIndex];
        handshape = cursorIndex;
        seperatingLine.SetActive(true);

        popupText.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX, posY);
    }

    void DisablePopupUI()
    {
        popupText.text = "";
        popupImage.sprite = popupIcons[(int)ui_img.none];
        handshape = (int)cursor_img.none;
        seperatingLine.SetActive(false);
        popupText.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(538f, 10f);
        
    }




}
