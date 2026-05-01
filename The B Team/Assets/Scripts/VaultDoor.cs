using UnityEngine;
using System.Collections;

public class VaultDoor : MonoBehaviour
{
    [SerializeField] Room4_Microwave microwaveScript;
    [SerializeField] Animator animator;
    [SerializeField] GameObject explosionParticles;
    public Transform spawnPoint;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] AudioClip[] clips;

    private bool explosionStarted = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (microwaveScript == null) { return; }


        if (microwaveScript.Primed && !explosionStarted)
        {
            explosionStarted=true;
            StartCoroutine(ExplodeDoor());
        }
        
    }

    private IEnumerator ExplodeDoor()
    {
        //play sound 1
        yield return new WaitForSeconds(1f);
        audioSource.PlayOneShot(clips[0], audioSource.volume * GlobalSettings.SFXVolume);
        yield return new WaitForSeconds(4.1f);
        audioSource.PlayOneShot(clips[1], audioSource.volume * GlobalSettings.SFXVolume);
        yield return new WaitForSeconds(0.9f);
        ObjectiveManager.Instance.CompleteObjective("Find a way to blast through the vault door.", "Finish the job...");
        animator.SetTrigger("ExplodeDoor");
        RemoveMicrowaveAndCutlery();

    }
    public void RemoveMicrowaveAndCutlery()
    {
        Destroy(GameObject.Find("Room4_Microwave"));
        Destroy(GameObject.Find("Cutlery"));
        var ps = Instantiate(explosionParticles, spawnPoint.position, Quaternion.identity);
        GetComponent<VaultDoor>().enabled = false;
    }
}
