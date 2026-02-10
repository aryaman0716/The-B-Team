using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Queue<DialogueEntry> sentences;

    public Text nameText;
    public Text dialogueText;
    public Image portraitImage;

    public Animator animator;
    public AudioClip wa;
    public AudioSource talkSounds;

    void Start()
    {
        sentences = new Queue<DialogueEntry>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("Open", true);
        //Time.timeScale = 0f;

        Debug.Log("Starting conversation");

        sentences.Clear();

      int setIndex = 0;
        
       foreach (DialogueEntry entry in dialogue.dialogueSets[setIndex].entries)
       {
         sentences.Enqueue(entry);
       }
       DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueEntry entry = sentences.Dequeue();

        nameText.text = entry.speaker;
        portraitImage.sprite = entry.portrait;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(entry.sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
            talkSounds.PlayOneShot(wa);
        }

        yield return new WaitForSeconds(2f);

        DisplayNextSentence();
    }


    public void EndDialogue()
    {
        animator.SetBool("Open", false);
        Time.timeScale = 1f;
    }
}
