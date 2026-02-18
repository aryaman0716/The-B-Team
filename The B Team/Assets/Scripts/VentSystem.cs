using UnityEngine;
public class VentSystem : MonoBehaviour
{
    public GameObject vent;
    public void ActivateVent()
    {
        Debug.Log("Vent activated!");
        if (vent != null)
        {
            vent.SetActive(true);
        }
    }
}
