using UnityEngine;
public class VentCover : MonoBehaviour
{
    public VentSystem ventSystem;
    public VentFocusController focusController;
    public DialogueTrigger Dialoguetrigger;
    private GameObject Player;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    void OnMouseOver()
    {
        if (Player == null) return;
        if (((Player.transform.position - transform.position).magnitude > 3f)) return;
        Debug.Log("Vent cover clicked");
        if (!ventSystem.isActivated)
        {
            Dialoguetrigger.TriggerDialogue();
            return;
        }
        if (Input.GetMouseButtonDown(1))
        {
            focusController.EnterFocusMode();
        }
    }
}
