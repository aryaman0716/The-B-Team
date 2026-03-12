using UnityEngine;

public class ManagerDoor : MonoBehaviour
{
    public Animator animator;
    public DialogueTrigger dia;
    private bool opened = false;
    private GameObject Player;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    
    public void UnlockAndOpen()
    {
        if (opened) return;
        opened = true;

        if (animator != null)
        {
            animator.SetTrigger("Open");
        }
        Debug.Log("Door Unlocked and Opening!");
    }

    private void OnMouseOver()
    {
        if (Player == null || opened) return;
        if ((Player.transform.position - transform.position).magnitude > 3f) return;

        if (dia != null) dia.TriggerDialogue();
    }
}