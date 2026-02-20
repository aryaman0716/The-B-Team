using UnityEngine;
public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance;

    [Header("Cursor Textures")]
    public Texture2D openHandCursor;
    public Texture2D closeHandCursor;
    private enum CursorState
    {
        Normal,
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
            return;
        }
    }
    public void SetNormal()
    {
        if (currentState == CursorState.Normal) return;

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        currentState = CursorState.Normal;
    }
    public void SetOpenHand()
    {
        if (currentState == CursorState.OpenHand) return;

        ApplyCursor(openHandCursor, CursorState.OpenHand);
    }
    public void SetCloseHand()
    {
        if (currentState == CursorState.CloseHand) return;

        ApplyCursor(closeHandCursor, CursorState.CloseHand);
    }
    private void ApplyCursor(Texture2D texture, CursorState state)
    {
        if (texture == null) return;

        Vector2 hotspot = new Vector2(texture.width / 2f, texture.height / 2f);
        Cursor.SetCursor(texture, hotspot, CursorMode.Auto);

        currentState = state;
    }
}