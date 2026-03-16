using UnityEngine;
public class VentFlourTarget : MonoBehaviour
{
    public GameObject lasersParent;
    public VentSystem vent;
    public void RevealLasers()
    {
        if (!vent.ventOpened) return;
        foreach (Renderer r in lasersParent.GetComponentsInChildren<Renderer>())
        {
            r.enabled = true; // Show the lasers
        }
    }
}
