using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using TMPro;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform[] checkpoints;
    [SerializeField] private ScreenFade screenFade;
    [SerializeField] private GameObject deathTextObj;
    private TMP_Text deathText;
    private bool respawning;

    private FPController controller;
    private CharacterController characterController;
    private Transform defaultSpawnPoint;

    private Dictionary<string,string> deathTextList = new Dictionary<string, string>();
    private void initialiseDeathTextList()
    {
        deathTextList.Add("laser", "Caution tape means,'take caution'!");
        deathTextList.Add("pit", "Your ankles <i>are</i> infact needed for the heist, unfortunately!");
        deathTextList.Add("sauce", "You got lost in the sauce!");
        deathTextList.Add("camera", "Bet you didn't C<s>(CTV)</s> that coming..?");
        deathTextList.Add("elevator", "You're gettin' Soft... Lock in!");
        deathTextList.Add("chute", "You're not product? Why are you going down there!?");
    }

    private void Start()
    {
        controller = GetComponent<FPController>();
        characterController = GetComponent<CharacterController>();
        defaultSpawnPoint = spawnPoint;
        initialiseDeathTextList();
        deathText = deathTextObj.GetComponent<TMP_Text>();
        deathTextObj.SetActive(false);
        int checkpointIndex = PlayerPrefs.GetInt("CheckpointIndex", 0);
        if (checkpointIndex < checkpoints.Length)
        {
            spawnPoint = checkpoints[checkpointIndex];
            characterController.enabled = false;
            transform.position = spawnPoint.position;  // move the player to the respective checkpoint position 
            characterController.enabled = true;
        }

        ObjectiveManager objManager = FindFirstObjectByType<ObjectiveManager>();
        if (objManager != null)
        {
            objManager.SetObjectiveFromCheckpoint(checkpointIndex);
        }
    }
    public void SetCheckpoint(Transform checkpoint)
    {
        if (checkpoint == null)
        {
            return;
        }
        spawnPoint = checkpoint;
    }
    public void ResetToDefaultCheckpoint()
    {
        if (defaultSpawnPoint != null)
        {
            spawnPoint = defaultSpawnPoint;
        }
    }
    public void Respawn(string id)
    {
        if (respawning) { return; }
        var storedVolume = GlobalSettings.SFXVolume;
        GlobalSettings.SFXVolume = 0f;
        respawning = true;
        StartCoroutine(RespawnRoutine(id, storedVolume));
        Animator animator = GameObject.FindGameObjectWithTag("UIDeath").GetComponent<Animator>();
        deathText.text = "";
        deathTextObj.SetActive(true);
        Animator animator2 = deathTextObj.GetComponent<Animator>();
        animator.SetTrigger("Play");
        animator2.SetTrigger("wiggleText");
    }
    private IEnumerator RespawnRoutine(string id, float storedVolume)
    {
        DialogueManager diabox = GameObject.Find("DialogueBox").GetComponent<DialogueManager>();
        if (diabox != null && DialogueManager.dialoguePlaying) {diabox.EndDialogue(); }
        yield return screenFade.FadeOut();
        yield return new WaitForSeconds(0.1f);
        
        controller.enabled = false;
        controller.SetCanMove(false);
        characterController.enabled = false;
        
        yield return new WaitForSeconds(0.2f);

        string val;
        if (deathTextList.TryGetValue(id, out val)) { deathText.text = val; }
        else { deathText.text = ""; }
        
        yield return new WaitForSeconds(0.8f);

        transform.position = spawnPoint.position;

        yield return new WaitForSeconds(0.5f);

        if (deathText.text != "") { deathText.gameObject.SetActive(false); }

        yield return screenFade.FadeIn();

        if(DialogueManager.lastDialogue != null) { GameObject.Find("DialogueBox").GetComponent<DialogueManager>().StartDialogue(DialogueManager.lastDialogue); }
        else {GameObject.Find("DialogueBox").GetComponent<DialogueManager>().StartDialogue(DialogueManager.currentDialogue); }

        characterController.enabled = true;
        controller.enabled = true;
        controller.ResetPlayerState();
        controller.SetCanMove(true);
        GlobalSettings.SFXVolume = storedVolume;
        respawning = false;
    }
}


