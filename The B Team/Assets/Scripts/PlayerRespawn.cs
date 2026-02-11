using UnityEngine;
using System.Collections;
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
    }
    public void Respawn()
    {
        StartCoroutine(RespawnRoutine());
    }
    private IEnumerator RespawnRoutine()
    {
        controller.SetCanMove(false);
        yield return screenFade.FadeOut();  

        characterController.enabled = false; 
        transform.position = spawnPoint.position;  // Move the player to the respawn point.
        transform.rotation = spawnPoint.rotation; 
        characterController.enabled = true;

        yield return new WaitForSeconds(0.2f);  // small delay before fading back in 
        yield return screenFade.FadeIn();
        controller.SetCanMove(true);
    }
}
