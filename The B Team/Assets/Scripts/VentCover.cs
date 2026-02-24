using UnityEngine;
public class VentCover : MonoBehaviour
{
    public VentSystem ventSystem;
    public VentFocusController focusController;
    public DialogueTrigger Dialoguetrigger;
    void OnMouseOver()
    {
        Dialoguetrigger.TriggerDialogue();
        Debug.Log("Vent cover clicked");
        if (!ventSystem.isActivated) return;
        if (Input.GetMouseButtonDown(1))
        {
            focusController.EnterFocusMode();
        }
    }
}
