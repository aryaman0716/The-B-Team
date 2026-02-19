using UnityEngine;
public class VentCover : MonoBehaviour
{
    public VentSystem ventSystem;
    public VentFocusController focusController;
    void OnMouseDown()
    {
        Debug.Log("Vent cover clicked");
        if (!ventSystem.isActivated) return;
        focusController.EnterFocusMode();
    }
}
