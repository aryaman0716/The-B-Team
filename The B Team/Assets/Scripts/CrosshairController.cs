using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        image.sprite = sprites[handshape];
        bool overUI = IsPointerOverUI();

        if (Pickup.carrying)
        {
            handshape = 3;
        }
        else if (Pickup.mousing || (UIController.Paused && !overUI))
        {
            handshape = 1;
        }
        else if (KeypadButtonScript.mousingB || MicrowaveController.mousingM || SinkInteractable.mousingS || (UIController.Paused && overUI))
        {
            handshape = 2;
        }
        else
        {
            handshape = 0;
        }

        if (anyFocus || UIController.Paused)
        {
            FollowMouse();
        }
        else
        {
            CenterCrosshair();
        }


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
}
