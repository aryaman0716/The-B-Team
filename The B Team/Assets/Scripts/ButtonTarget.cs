using UnityEngine;
public class ButtonTarget : MonoBehaviour
{
    public VentSystem ventToActivate;
    public DialogueTrigger dia;
    public WireActivate relatedWire;
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("TomatoProjectile"))
        {
            if (ventToActivate != null)
            {
                dia.TriggerDialogue();
                ventToActivate.ActivateVent();
                relatedWire.ActivateWire();
            }
            ObjectiveManager.Instance.CompleteObjective("Find a way to power on the ventilation system.");
            ObjectiveManager.Instance.SetObjective("Find a way to reveal lasers.");
        }
    }
}
