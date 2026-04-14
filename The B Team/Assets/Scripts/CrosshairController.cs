using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class CrosshairController : MonoBehaviour
{
    private Image cursorImage;
    [SerializeField] private Sprite[] cursorSprites;
    [SerializeField] private Sprite[] popupIcons;
    public static int handshape = 0;
    public static bool anyFocus = false;
    private RectTransform rectTransform;
    private Canvas canvas;
    [SerializeField] private Vector2 pausedOffset;
    [SerializeField] private Vector2 normalOffset;

    private Image popupImage;
    private TMP_Text popupText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cursorImage = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        InitialisePopupUI();
    }

    // Update is called once per frame
    void Update()
    {
        cursorImage.sprite = cursorSprites[handshape];
        bool overUI = IsPointerOverUI();

        //if (Pickup.carrying)
        //{
        //    handshape = 3;
        //}
        //else if (Pickup.mousing || (UIController.Paused && !overUI))
        //{
        //    handshape = 1;
        //}
        //else if (KeypadButtonScript.mousingB || 
        //        MicrowaveController.mousingM || 
        //        SinkInteractable.mousingS || 
        //        (UIController.Paused && overUI))
        //{
        //    handshape = 2;
        //}
        //else
        //{
        //    handshape = 0;
        //}
        HandleUI();
        if (anyFocus || UIController.Paused)
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
        if(UIController.Paused && IsPointerOverUI())
        {
            handshape = 2;
            popupText.text = "";
            return;
        }
        if (Pickup.carrying)
        {
            handshape = 3;
            popupImage.sprite = popupIcons[2];
            popupText.text = "Throw";
            return;
        }
        if (Pickup.mousing)
        {
            popupImage.sprite = popupIcons[1];
            handshape = 1;
            popupText.text = "Grab";
            return;
        }
        if (GeneralDoor.currentDoor != null)
        {
            
            if (GameObject.Find("Room1ExitDoor").GetComponent<GeneralDoor>() == GeneralDoor.currentDoor && GeneralDoor.currentDoor.locked)
            {
                popupImage.sprite = popupIcons[0];
                popupText.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(18f, -100.5f);
                handshape = 4;
                popupText.text = "Jammed...";
                return;
            }
            if (GeneralDoor.currentDoor.locked == true)
            {
                popupImage.sprite = popupIcons[0];
                popupText.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(18f, -100.5f);
                handshape = 4;
                popupText.text = "It's locked...";
                return;
            }
            if (GeneralDoor.currentDoor.opened)
            {
                popupImage.sprite = popupIcons[1];
                handshape = 1;
                popupText.text = "Close";
                return;
            }
            else
            {
                popupImage.sprite = popupIcons[1];
                handshape = 1;
                popupText.text = "Open";
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
                    handshape = 3;
                    popupText.text = "Knead";
                    popupImage.sprite = popupIcons[1];
                    return;
                }
                handshape = 0;
                popupText.text = "";
                popupImage.sprite = popupIcons[0];
                return;
            }
            handshape = 2;
            if (obj.IsOn)
            {
                popupImage.sprite = popupIcons[1];
                popupText.text = ("Turn Off");
                return;
            }
            else
            {
                popupImage.sprite = popupIcons[1];
                popupText.text = ("Turn On");
                return;
            }
        }
        if (MicrowaveController.mousingM)
        {
            var obj = GameObject.Find("Microwave").GetComponent<MicrowaveController>();
            if (obj == null || obj.enabled == false || obj.KeyCooked) { return; }
            
            if (obj.open)
            {
                popupImage.sprite = popupIcons[1];
                handshape = 1;
                popupText.text = "Close";
                return;
            }
            else
            {
                popupImage.sprite = popupIcons[1];
                handshape = 3;
                popupText.text = "Open";
                return;
            }
        }
        if (KeypadButtonScript.mousingB)
        {
            popupImage.sprite = popupIcons[1];
            handshape = 2;
            popupText.text = "Push";
            return;
        }

        handshape = 0;
        popupText.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(18f, -180);
        popupImage.sprite = popupIcons[0];
        popupText.text = "";
    }

    void InitialisePopupUI()
    {
        popupText = GameObject.Find("popupText").GetComponent<TMP_Text>();
        popupImage = GameObject.Find("popupImage").GetComponent<Image>();
        
    }

    
}
