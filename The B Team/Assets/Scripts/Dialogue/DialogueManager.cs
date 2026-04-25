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

    public static bool dialoguePlaying;
    public DialogueEntry[] onReplayDialogue;
    public static Dialogue currentDialogue;
    public static Dialogue lastDialogue;
    public Animator boxAnimator;

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
        
        currentDialogue = dialogue;
        talkSounds.volume = (0.5f * GlobalSettings.MasterVolume * GlobalSettings.SFXVolume);
        animator.SetBool("Open", true);
        dialoguePlaying = true;
        Debug.Log(dialoguePlaying + " dialogue playing");
        Debug.Log("Starting conversation");

        sentences.Clear();

      int setIndex = 0;
        
       foreach (DialogueEntry entry in dialogue.dialogueSets[setIndex].entries)
       {
         sentences.Enqueue(entry);
       }
       if (animator == null)
       {
            DisplayNextSentence();
       }
       if (cutScene)
            AttemptNext();
    }
    public void ReplayDialogue()
    {
        if (lastDialogue == null) { return; }
        if (lastDialogue.dialogueSets == null || lastDialogue.dialogueSets.Length == 0) { return; }
        
        DialogueSet lastSet = lastDialogue.dialogueSets[0];

        if (lastSet.entries == null) { return; }

        DialogueEntry[] lastEntries = lastSet.entries;
        DialogueEntry[] newEntries = new DialogueEntry[lastEntries.Length + 1];

        newEntries[0] = onReplayDialogue[Random.Range(0, onReplayDialogue.Length)];

        for(int i = 0; i < lastEntries.Length; i++)
        {
            newEntries[i + 1] = lastEntries[i];
        }

        Dialogue dialogue = new Dialogue();
        DialogueSet dialogueSet = new DialogueSet();

        dialogueSet.entries = newEntries;
        dialogue.dialogueSets = new DialogueSet[] { dialogueSet };
        StartDialogue(dialogue);

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
        bool notReplayDialogue = false;
        for (int i = 0; i < onReplayDialogue.Length; i++)
        {
            if (currentDialogue.dialogueSets[0].entries[0] == onReplayDialogue[i])
            {
                notReplayDialogue = false;
                break;
            }
            else
            {
                notReplayDialogue = true;
            }
        }
        if (notReplayDialogue) { lastDialogue = currentDialogue; }
        currentDialogue = null;
        talkSounds.volume = 0f;
        animator.SetBool("Open", false);
        
        Time.timeScale = 1f;
        if (cutScene)
        {
            SceneManager.LoadScene(nextScene);
        }
        dialoguePlaying = false;
        Debug.Log(dialoguePlaying + " dialogue playing");
    }

    public void AttemptNext()
    {
        DisplayNextSentence();
        sentenceDone = false;
        if (boxAnimator != null) boxAnimator.SetTrigger("Wobble");
    }
}
