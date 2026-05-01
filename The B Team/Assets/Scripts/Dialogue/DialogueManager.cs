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

    public static bool dialoguePlaying = false;
    public DialogueEntry[] onReplayDialogue;
    public bool replayingDialogue = false;
    public static Dialogue currentDialogue;
    public static Dialogue lastDialogue;
    public Animator boxAnimator;
    public CutsceneController cSController;

    void Awake()
    {
        sentences = new Queue<DialogueEntry>();
        dialoguePlaying = false;
        replayingDialogue = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (cutScene)
                AttemptNext();
        }
        if (Input.GetKey(KeyCode.Q))
        {
            dialogueSpeedMod = 8f;
        }
        else
        {
            dialogueSpeedMod = 2f;
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (dialogue == null) { Debug.LogWarning("Dialogue not input"); return; }
        sentences.Clear();
        currentDialogue = dialogue;
        talkSounds.volume = (0.5f * GlobalSettings.MasterVolume * GlobalSettings.SFXVolume);
        animator.SetBool("Open", true);
        dialoguePlaying = true;

        Debug.Log("Starting conversation");

        int setIndex = 0;

        if (!replayingDialogue)
        {
            foreach (DialogueEntry entry in dialogue.dialogueSets[setIndex].entries)
            {
                sentences.Enqueue(entry);
            }
        }
        else
        {
            DialogueSet replaySet = AppendDialogueToStart(onReplayDialogue[0], dialogue.dialogueSets[setIndex]);
            if(replaySet == null) { Debug.Log("Replay set is null. Exiting function."); return; }
            foreach(DialogueEntry entry in replaySet.entries)
            {
                sentences.Enqueue(entry);
            }
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
        if (replayingDialogue) { Debug.Log("Already replaying dialogue"); return; }
        Debug.Log("Attempting to replay dialogue");
        if(lastDialogue == null) { Debug.Log("Last dialogue not found"); return; }
        replayingDialogue = true;
        StartDialogue(lastDialogue);
    }

    public DialogueSet AppendDialogueToStart(DialogueEntry entry, DialogueSet set)
    {
        if(set.entries == null) { Debug.Log("No entries in dialogue set"); return null; }

        DialogueEntry[] setEntries = set.entries;
        DialogueEntry[] newEntries = new DialogueEntry[setEntries.Length + 1];

        newEntries[0] = entry;
        
        for(int i = 0; i < setEntries.Length; i++)
        {
            newEntries[i + 1] = setEntries[i];
        }

        return new DialogueSet { entries = newEntries };
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
        if (!replayingDialogue) { lastDialogue = currentDialogue; }
        else { Debug.Log("Replay dialogue is playing. Current dialogue = null"); }
        StopAllCoroutines();
        currentDialogue = null;
        replayingDialogue = false;
        talkSounds.volume = 0f;
        animator.SetBool("Open", false);
        
        Time.timeScale = 1f;
        if (cutScene)
        {
            if (cSController != null)
            {
                cSController.EndCutscene();
            }
            else
            {
                SceneManager.LoadScene(nextScene);
            }

            }
        dialoguePlaying = false;
    }

    public void AttemptNext()
    {
        DisplayNextSentence();
        sentenceDone = false;
        if (boxAnimator != null) boxAnimator.SetTrigger("Wobble");
    }
}
