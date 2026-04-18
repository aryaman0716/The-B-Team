using UnityEngine;
using TMPro;

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
        //TMP_Text fText = GetComponentInChildren<TMP_Text>();
        //if (fText != null)
        //{
        //    fText.text = "Oiled Door";
        //}
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
        ObjectiveManager.Instance.CompleteObjective("Find something to oil the door.", "Find a way to unlock the shutter.");
        if (animator != null)
        {
            animator.SetTrigger("Open");
        }
    }
}
