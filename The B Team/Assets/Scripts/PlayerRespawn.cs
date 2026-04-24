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
    [SerializeField] private TMP_Text deathText;

    private FPController controller;
    private CharacterController characterController;
    private Transform defaultSpawnPoint;

    private Dictionary<string,string> deathTextList = new Dictionary<string, string>();
    private void initialiseDeathTextList()
    {
        deathTextList.Add("laser", "Caution tape means,'take caution'!");
        deathTextList.Add("pit", "Nothing down there!");
        deathTextList.Add("sauce", "You burned up!");
        deathTextList.Add("camera", "Cameras are your number one enemy if you're stealing stuff..!");
        deathTextList.Add("chute", "You're not product? Why are you going down there!?");
    }

    private void Start()
    {
        controller = GetComponent<FPController>();
        characterController = GetComponent<CharacterController>();
        defaultSpawnPoint = spawnPoint;
        initialiseDeathTextList();
        screenFade = GameObject.FindAnyObjectByType(typeof(ScreenFade)) as ScreenFade;
        deathText = GameObject.Find("deathText").GetComponent<TMP_Text>();
        deathText.gameObject.SetActive(false);

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
        StartCoroutine(RespawnRoutine(id));
        Animator animator = GameObject.FindGameObjectWithTag("UIDeath").GetComponent<Animator>();
        animator.SetTrigger("Play");
    }
    private IEnumerator RespawnRoutine(string id)
    {
        GameObject.Find("DialogueBox").GetComponent<DialogueManager>().EndDialogue();
        
        yield return screenFade.FadeOut();
        yield return new WaitForSeconds(0.1f);
        
        controller.enabled = false;
        controller.SetCanMove(false);
        characterController.enabled = false;
        
        yield return new WaitForSeconds(0.2f);

        string val;
        if (deathTextList.TryGetValue(id, out val)) { deathText.text = val; }
        else { deathText.text = ""; }
        if (deathText.text != "") { deathText.gameObject.SetActive(true); }
        else { deathText.gameObject.SetActive(false); }
        
        yield return new WaitForSeconds(0.8f);

        transform.position = spawnPoint.position;

        yield return new WaitForSeconds(0.5f);

        if (deathText.text != "") { deathText.gameObject.SetActive(false); }

        yield return screenFade.FadeIn();

        if(DialogueManager.lastDialogue != null) { GameObject.Find("DialogueBox").GetComponent<DialogueManager>().StartDialogue(DialogueManager.lastDialogue); }
        else { GameObject.Find("DialogueBox").GetComponent<DialogueManager>().StartDialogue(DialogueManager.currentDialogue); }

        characterController.enabled = true;
        controller.enabled = true;
        controller.ResetPlayerState();
        controller.SetCanMove(true);
    }
}


