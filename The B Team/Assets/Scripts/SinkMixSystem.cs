using UnityEngine;
using TMPro;

public class SinkMixSystem : MonoBehaviour
{
    [Header("References")]
    public SinkInteractable sinkInteractable;
    public GameObject doughPrefab;
    public Transform doughSpawnPoint;

    private bool flourAdded = false;
    private bool doughCreated = false;
    public void AddFlour()
    {
        if (!sinkInteractable.IsOn())
        {
            Debug.Log("Water is not running! Cannot add flour.");
            return;
        }
        if (flourAdded)
        {
            Debug.Log("Flour already added!");
            return;
        }
        
        flourAdded = true;
        Debug.Log("Flour added to water!");
        TMP_Text fText = GetComponentInChildren<TMP_Text>();
        if (fText != null) 
        {
            fText.text = "Sink + Flour";
        }
    }
    public void MixWithSpatula()
    {
        if (!flourAdded)
        {
            Debug.Log("Flour not added yet! Cannot mix.");
            return;
        }
        if (doughCreated)
        {
            return;
        }
        doughCreated = true;
        Debug.Log("Dough created!");
        if (doughPrefab != null && doughSpawnPoint != null)
        {
            Instantiate(doughPrefab, doughSpawnPoint.position, Quaternion.identity);
        }
    }
}
