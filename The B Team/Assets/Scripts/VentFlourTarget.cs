using UnityEngine;
using System.Collections;

public class VentFlourTarget : MonoBehaviour
{
    public GameObject lasersParent;
    public VentSystem vent;
    public AudioSource ventAudioSource;
    private bool lasersFading;
    public bool LasersFading => lasersFading;

    public static bool mousing;
    void Update()
    {
        GetComponent<Outline>().enabled = false;
    }
    void OnMouseOver()
    {
        if (UIController.Paused) { GetComponent<Outline>().enabled = false; mousing = false; }
        if (EquipmentController.DistanceToPlayer(transform) > 5f) 
        {
            GetComponent<Outline>().enabled = false;
            mousing = false;
            return; 
        }
        else
        {
            GetComponent<Outline>().enabled = true;
            mousing = true;
        }
    }
    void OnMouseExit()
    {
        mousing = false;
        if (EquipmentController.DistanceToPlayer(transform) > 5f)
        {
            GetComponent<Outline>().enabled = false;
            mousing = false;
            return;
        }
        GetComponent<Outline>().enabled = false;
        mousing = false;
    }
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
        ventAudioSource.volume = (0.9f * GlobalSettings.SFXVolume * GlobalSettings.MasterVolume);
        ventAudioSource.loop = false;
        
        yield return new WaitForSeconds(1.5f);
        var alpha = 0f;
        while (true)
        {
            
            foreach (Renderer r in lasersParent.GetComponentsInChildren<Renderer>())
            {
                Color c = r.material.color;
                r.material.SetColor("_BaseColor", new Color(c.r, c.g, c.b, alpha));

            }

            alpha = (alpha*1.2f) + 0.01f;
            
            if (alpha >= 1f)
            {
                StopAllCoroutines();
            }
            yield return new WaitForSeconds(0.1f);
        }
        
    }
}
