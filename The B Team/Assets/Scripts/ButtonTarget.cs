using UnityEngine;
public class ButtonTarget : MonoBehaviour
{
    public VentSystem ventToActivate;
    public DialogueTrigger dia;
    public WireActivate relatedWire;

    private bool on;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("TomatoProjectile"))
        {
            if (!on && ventToActivate != null)
            {
                on = true;
                dia.TriggerDialogue();
                ventToActivate.ActivateVent();
                relatedWire.ActivateWire();
                GetComponent<AudioSource>().volume = (0.5f * GlobalSettings.SFXVolume * GlobalSettings.MasterVolume);
                GetComponent<AudioSource>().Play();
            }
            ObjectiveManager.Instance.CompleteObjective("Find a way to power on the ventilation system.", "Find a way to reveal lasers.");
        }
    }
}
