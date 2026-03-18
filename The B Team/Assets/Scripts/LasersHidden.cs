using UnityEngine;
public class LasersHidden : MonoBehaviour
{
    void Start()
    {
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.enabled = false; // Hide the lasers
        }
        GameObject.Find("FlourFog").GetComponentInChildren<ParticleSystem>().Stop();
    }

    void Update()
    {
    }
}