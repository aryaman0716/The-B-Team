using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public Queue<DialogueEntry> sentences;

    public Text nameText;
    public Text dialogueText;
    public Image portraitImage;

    public Animator animator;
    public AudioClip wa;
    public AudioSource talkSounds;
    public bool cutScene = false;
    public bool sentenceDone = false;
    public string nextScene;
    public float dialogueSpeedMod = 2f;

    void Start()
    {
        sentences = new Queue<DialogueEntry>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (cutScene)
                AttemptNext();
        }
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
            if (letter == ' ')
            {
                yield return new WaitForSeconds(0.05f / dialogueSpeedMod);
            }
            else
            {
                yield return new WaitForSeconds(0.05f / dialogueSpeedMod);
                talkSounds.volume = (0.5f * GlobalSettings.MasterVolume * GlobalSettings.SFXVolume);
                talkSounds.PlayOneShot(wa);
            }
        }

        if (!cutScene)
        {
            yield return new WaitForSeconds(2f);
            DisplayNextSentence();
        }
        else
        {
            sentenceDone = true;

        }
       
    }


    public void EndDialogue()
    {
        animator.SetBool("Open", false);
        Time.timeScale = 1f;
        if (cutScene)
        {
            SceneManager.LoadScene(nextScene);
        }
    }

    public void AttemptNext()
    {
        DisplayNextSentence();
        sentenceDone = false;
    }
}
