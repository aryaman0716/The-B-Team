using UnityEngine;
public class VentCover : MonoBehaviour
{
    public VentSystem ventSystem;
    public VentFocusController focusController;
    void OnMouseOver()
    {
        Debug.Log("Vent cover clicked");
        if (!ventSystem.isActivated) return;
        if (Input.GetMouseButtonDown(1))
        {
            focusController.EnterFocusMode();
        }
    }
}
