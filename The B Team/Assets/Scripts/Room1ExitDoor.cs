using UnityEngine;
public class Room1ExitDoor : MonoBehaviour
{
    public Animator animator;
    private bool opened = false;
    public DialogueTrigger dia;
    private GameObject Player;
    public bool oiled = false;
    public void ApplyOil()
    {
        oiled = true;

    }
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnMouseOver()
    {
        if (Player == null) return;
        if (((Player.transform.position - transform.position).magnitude > 3f)) return;
        
        dia.TriggerDialogue();
        return;
    }
    public void Pry()
    {
        if (!oiled) return;
        if (opened) return;
        opened = true;
        if (animator != null)
        {
            animator.SetTrigger("Open");
        }
    }
}
