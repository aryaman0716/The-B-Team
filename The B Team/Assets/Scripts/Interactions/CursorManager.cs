using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance;

    [Header("Cursor Settings")]
    public Texture2D defaultCursor;
    public Texture2D openHandCursor;
    public Texture2D closeHandCursor;

    
    private Vector2 hotspot = new Vector2(90, 180);

    private enum CursorState { Default, OpenHand, CloseHand }
    private CursorState currentState;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        SetDefault();
    }

    
    void Update()
    {
        if (!Cursor.visible)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void SetDefault()
    {
        
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        currentState = CursorState.Default;
    }

    public void SetOpenHand()
    {
        Cursor.SetCursor(openHandCursor, hotspot, CursorMode.Auto);
        currentState = CursorState.OpenHand;
    }

    public void SetCloseHand()
    {
        Cursor.SetCursor(closeHandCursor, hotspot, CursorMode.Auto);
        currentState = CursorState.CloseHand;
    }


    
    public void ToggleCursor(bool isFree)
    {
        if (isFree)
        {
            Cursor.lockState = CursorLockMode.None; 
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; 
            Cursor.visible = false; 
        }
    }

}