using UnityEngine;
public class LasersHidden : MonoBehaviour
{
    void Start()
    {
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            var colour = r.material.color;
            r.material.SetColor("_BaseColor", new Color(colour.r, colour.g, colour.b, 0));
        }
        GameObject.Find("FlourFog").GetComponentInChildren<ParticleSystem>().Stop();
    }

    void Update()
    {
    }
}