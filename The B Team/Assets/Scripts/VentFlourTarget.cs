using UnityEngine;
using System.Collections;

public class VentFlourTarget : MonoBehaviour
{
    public GameObject lasersParent;
    public VentSystem vent;
    private bool lasersFading;
    public void RevealLasers()
    {
        if (!vent.ventOpened || !vent.isActivated || lasersFading) return;
        if (!lasersFading) { StartCoroutine(FadeLasers()); }
        
        ObjectiveManager.Instance.CompleteObjective("Find a way to reveal lasers.", "Find something to oil the door.");
        if (GameObject.Find("FlourFog").GetComponentInChildren<ParticleSystem>() != null) { GameObject.Find("FlourFog").GetComponentInChildren<ParticleSystem>().Play(); }
    }

    private IEnumerator FadeLasers()
    {
        if (!lasersFading) { lasersFading = true; }
        var alpha = 0f;
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            
            foreach (Renderer r in lasersParent.GetComponentsInChildren<Renderer>())
            {
                Color c = r.material.color;
                r.material.SetColor("_BaseColor", new Color(c.r, c.g, c.b, alpha));

            }
            Debug.Log(alpha);
            alpha += 0.01f;
            if(alpha >= 1f)
            {
                StopAllCoroutines();
            }
            yield return new WaitForSeconds(0.1f);
        }
        
    }
}
