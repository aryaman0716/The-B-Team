using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;
public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform[] checkpoints;
    [SerializeField] private ScreenFade screenFade;

    private FPController controller;
    private CharacterController characterController;
    private Transform defaultSpawnPoint;

    private void Start()
    {
        controller = GetComponent<FPController>();
        characterController = GetComponent<CharacterController>();
        defaultSpawnPoint = spawnPoint;

        screenFade = GameObject.FindAnyObjectByType(typeof(ScreenFade)) as ScreenFade;

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
    public void Respawn()
    {
        StartCoroutine(RespawnRoutine());
        Animator animator = GameObject.FindGameObjectWithTag("UIDeath").GetComponent<Animator>();
        animator.SetTrigger("Play");
    }
    private IEnumerator RespawnRoutine()
    {
        GameObject.Find("DialogueBox").GetComponent<DialogueManager>().EndDialogue();
        yield return screenFade.FadeOut();
        yield return new WaitForSeconds(0.1f);
        controller.SetCanMove(false);
        controller.enabled = false;
        characterController.enabled = false;

        yield return new WaitForSeconds(1f);

        controller.ResetPlayerState();
        transform.position = spawnPoint.position;

        yield return new WaitForSeconds(0.5f);
        yield return screenFade.FadeIn();

        if(DialogueManager.lastDialogue != null) { GameObject.Find("DialogueBox").GetComponent<DialogueManager>().StartDialogue(DialogueManager.lastDialogue); }
        else { GameObject.Find("DialogueBox").GetComponent<DialogueManager>().StartDialogue(DialogueManager.currentDialogue); }

            characterController.enabled = true;
        controller.enabled = true;
        controller.SetCanMove(true);
    }
}
