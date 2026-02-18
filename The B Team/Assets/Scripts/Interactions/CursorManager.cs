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
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        currentState = CursorState.Default;
    }
    public void SetOpenHand()
    {
        if (currentState == CursorState.OpenHand) return;
        Cursor.SetCursor(openHandCursor, Vector2.zero, CursorMode.Auto);
        currentState = CursorState.OpenHand;
    }
    public void SetCloseHand()
    {
        if (currentState == CursorState.CloseHand) return;
        Cursor.SetCursor(closeHandCursor, Vector2.zero, CursorMode.Auto);
        currentState = CursorState.CloseHand;
    }
}
