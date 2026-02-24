using UnityEngine;
public class Room1ExitDoor : MonoBehaviour
{
    public Animator animator;
    private bool opened = false;
    public DialogueTrigger dia;
    public void ApplyOil()
    {
        if (opened) return;
        opened = true;
        if (animator != null)
        {
            animator.SetTrigger("Open");
        }
    }
    private void OnMouseOver()
    {
        dia.TriggerDialogue();
    }
}
