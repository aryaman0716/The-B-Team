using UnityEngine;
public class ButtonTarget : MonoBehaviour
{
    public VentSystem ventToActivate;
    public void Activate()
    {
        if (ventToActivate != null)
        {
            ventToActivate.ActivateVent();
        }
    }
}
