using UnityEngine;
public class ButtonTarget : MonoBehaviour
{
    public VentSystem ventToActivate;
    public DialogueTrigger dia;
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("TomatoProjectile"))
        {
            if (ventToActivate != null)
            {
                dia.TriggerDialogue();
                ventToActivate.ActivateVent();
            }
        }
    }
}
