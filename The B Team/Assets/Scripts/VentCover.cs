using UnityEngine;
public class VentCover : MonoBehaviour
{
    public VentSystem ventSystem;
    public VentFocusController focusController;
    public DialogueTrigger Dialoguetrigger;
    private GameObject Player;

    private bool onGround = false;
    public AudioClip[] hitGroundSound;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    void OnMouseOver()
    {
        if (Player == null) return;
        if (((Player.transform.position - transform.position).magnitude > 3f)) return;
        Debug.Log("Vent cover clicked");
        if (!ventSystem.isActivated)
        {
            Dialoguetrigger.TriggerDialogue();
            return;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Ground")
        {
            Debug.Log("Vent hit floor");
            onGround = true;
            GetComponent<AudioSource>().volume = (0.5f * GlobalSettings.SFXVolume * GlobalSettings.MasterVolume);
            GetComponent<AudioSource>().PlayOneShot(hitGroundSound[Random.Range(0, hitGroundSound.Length)]);
        }
    }
}
