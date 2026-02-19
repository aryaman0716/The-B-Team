using UnityEngine;
public class ButtonTarget : MonoBehaviour
{
    public VentSystem ventToActivate;
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("TomatoProjectile"))
        {
            if (ventToActivate != null)
            {
                ventToActivate.ActivateVent();
            }
        }
    }
}
