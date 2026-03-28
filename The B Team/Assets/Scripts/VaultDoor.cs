using UnityEngine;
using System.Collections;

public class VaultDoor : MonoBehaviour
{
    [SerializeField] Room4_Microwave microwaveScript;
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem ps;

    private bool explosionStarted = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
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
        yield return new WaitForSeconds(1.5f);
        animator.SetTrigger("ExplodeDoor");

    }
    public void RemoveMicrowaveAndCutlery()
    {
        Destroy(GameObject.Find("Room4_Microwave"));
        Destroy(GameObject.Find("Cutlery"));
        ps.Play();
        GetComponent<VaultDoor>().enabled = false;
    }
}
