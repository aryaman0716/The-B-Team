using UnityEngine;
public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance;

    [Header("Cursor Settings")]
    public Texture2D defaultCursor;
    public Texture2D openHandCursor;
    public Texture2D closeHandCursor;
    private enum CursorState
    {
        Default,
        OpenHand,
        CloseHand
    }

    private CursorState currentState;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }
        else
        {
            Destroy(gameObject);
        }
        SetDefault();
    }
    public void SetDefault()
    {
        if (currentState == CursorState.Default) return;
        ApplyCursorSettings(defaultCursor, CursorState.Default);
    }

    public void SetOpenHand()
    {
        if (currentState == CursorState.OpenHand) return;
        ApplyCursorSettings(openHandCursor, CursorState.OpenHand);
    }

    public void SetCloseHand()
    {
        if (currentState == CursorState.CloseHand) return;
        ApplyCursorSettings(closeHandCursor, CursorState.CloseHand);
    }


    private void ApplyCursorSettings(Texture2D texture, CursorState state)
    {
       
        Vector2 hotspot = new Vector2(texture.width / 2, texture.height / 2);
        Cursor.SetCursor(texture, hotspot, CursorMode.Auto);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined; 
        currentState = state;
    }
}
