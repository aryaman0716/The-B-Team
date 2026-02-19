using UnityEngine;
public class VentCover : MonoBehaviour
{
    public VentSystem ventSystem;
    public VentFocusController focusController;
    void OnMouseDown()
    {
        Debug.Log("Vent cover clicked!");
        if (ventSystem == null) return;

        if (!ventSystem.isActivated)
        {
            Debug.Log("Vent is not activated yet. Hit the button with a tomato first!");
            return;
        }

        if (focusController != null)
        {
            focusController.EnterFocusMode();
        }
    }
}
