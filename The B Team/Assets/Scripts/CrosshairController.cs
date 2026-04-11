using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class CrosshairController : MonoBehaviour
{
    public Image image;
    public Sprite[] sprites;
    public static int handshape = 0;
    public static bool anyFocus = false;
    private RectTransform rectTransform;
    private Canvas canvas;
    [SerializeField] private Vector2 pausedOffset;
    [SerializeField] private Vector2 normalOffset;

    private TMP_Text popupText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        InitialisePopupUI();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(KeypadButtonScript.mousingB);
        image.sprite = sprites[handshape];
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
            popupText.text = "<b>(M2)\nThrow";
            return;
        }
        if (Pickup.mousing)
        {
            handshape = 1;
            popupText.text = "<b>(M1)\nGrab";
            return;
        }
        if (GeneralDoor.currentDoor != null)
        {
            handshape = 3;
            if (GameObject.Find("Room1ExitDoor").GetComponent<GeneralDoor>() == GeneralDoor.currentDoor && GeneralDoor.currentDoor.locked)
            {
                popupText.text = "Jammed...";
                return;
            }
            if (GeneralDoor.currentDoor.locked == true)
            {
                popupText.text = "It's locked...";
                return;
            }
            if (GeneralDoor.currentDoor.opened)
            {
                popupText.text = "<b>(M1)</b>\nClose";
                return;
            }
            else
            {
                popupText.text = "<b>(M1)</b>\nOpen";
                return;
            }
        }
        if (SinkInteractable.mousingS)
        {
            var obj = GameObject.Find("Sink_Base").GetComponent<SinkInteractable>();
            if (obj == null || obj.enabled == false) { return; }
            if (obj.Filled)
            {
                handshape = 0;
                popupText.text = "";
                return;
            }
            handshape = 2;
            if (obj.IsOn)
            {
                popupText.text = ("<b>(M1)</b>\nTurn Off");
                return;
            }
            else
            {
                popupText.text = ("<b>(M1)</b>\nTurn On");
                return;
            }
        }
        if (MicrowaveController.mousingM)
        {
            var obj = GameObject.Find("Microwave").GetComponent<MicrowaveController>();
            if (obj == null || obj.enabled == false || obj.KeyCooked) { return; }
            handshape = 2;
            if (obj.open)
            {
                popupText.text = "<b>(M1)</b>\nClose";
                return;
            }
            else
            {
                popupText.text = "<b>(M1)</b>\nOpen";
                return;
            }
        }
        if (KeypadButtonScript.mousingB)
        {
            handshape = 2;
            popupText.text = "<b>(M1)\nPush";
            return;
        }

        handshape = 0;
        popupText.text = "";
    }

    void InitialisePopupUI()
    {
        popupText = GameObject.Find("popupText").GetComponent<TMP_Text>();
        
    }

    
}
