using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public bool done = false;
    public bool triggerStart = false;

    private void Start()
    {
        if (triggerStart)
        {
            TriggerDialogue();
        }
    }
    public void TriggerDialogue()
    {
        if (!done)
        {
            FindFirstObjectByType<DialogueManager>().StartDialogue(dialogue);
            done = true;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerDialogue();
        }
    }
}
