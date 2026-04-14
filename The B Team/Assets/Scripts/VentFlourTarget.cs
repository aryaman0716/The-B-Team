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
            ObjectiveManager.Instance.CompleteObjective("Find a way to reveal lasers.", "Find something to oil the door.");
        }
        if (GameObject.Find("FlourFog").GetComponentInChildren<ParticleSystem>() != null) { GameObject.Find("FlourFog").GetComponentInChildren<ParticleSystem>().Play(); }
    }
}
