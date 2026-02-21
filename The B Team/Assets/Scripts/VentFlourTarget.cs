using UnityEngine;
public class VentFlourTarget : MonoBehaviour
{
    public GameObject lasersParent;
    public void RevealLasers()
    {
        foreach (Renderer r in lasersParent.GetComponentsInChildren<Renderer>())
        {
            r.enabled = true; // Show the lasers
        }
    }
}
