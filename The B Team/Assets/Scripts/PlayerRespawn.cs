using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;
public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private ScreenFade screenFade;

    private FPController controller;
    private CharacterController characterController;
    private void Start()
    {
        controller = GetComponent<FPController>();
        characterController = GetComponent<CharacterController>();

        screenFade = GameObject.FindAnyObjectByType(typeof(ScreenFade)) as ScreenFade;
    }
    public void Respawn()
    {
        StartCoroutine(RespawnRoutine());
        Animator animator = GameObject.FindGameObjectWithTag("UIDeath").GetComponent<Animator>();
        animator.SetTrigger("Play");
    }
    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        controller.SetCanMove(false);
        yield return new WaitForSeconds(0.5f);
        yield return screenFade.FadeOut();  
        
        characterController.enabled = false; 
        transform.position = spawnPoint.position;  // Move the player to the respawn point.
        characterController.enabled = true;
        controller.ResetPlayerState();

        yield return new WaitForSeconds(0.1f);
        yield return screenFade.FadeIn();
        controller.SetCanMove(true);
        controller.enabled = true;
    }
}
