using UnityEngine;
using TMPro;

public class SinkMixSystem : MonoBehaviour
{
    [Header("References")]
    public SinkInteractable sinkInteractable;
    public GameObject doughPrefab;
    public Transform doughSpawnPoint;
    private GameObject flour_obj;

    private bool flourAdded = false;
    private bool doughCreated = false;

    void Start()
    {
        flour_obj = GameObject.Find("flour_obj");
        flour_obj.SetActive(false);
    }

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
        flour_obj.SetActive(true);
        Debug.Log("Flour added to water!");
        //TMP_Text fText = GetComponentInChildren<TMP_Text>();
        //if (fText != null) 
        //{
        //    fText.text = "Sink + Flour";
        //}
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
        if (sinkInteractable.IsOn()) { sinkInteractable.ToggleSink(); }
        GetComponent<Collider>().enabled = false;
        Debug.Log("Dough created!");
        if (doughPrefab != null && doughSpawnPoint != null)
        {
            GameObject obj = Instantiate(doughPrefab, doughSpawnPoint.position, Quaternion.identity);
            obj.GetComponent<PlacementEmitter>().previewMeshes[0] = GameObject.Find("doughPreviewMeshSolid").GetComponent<MeshRenderer>();
            obj.GetComponent<PlacementEmitter>().previewHighlight = GameObject.Find("doughPreviewHighlight").GetComponent<MeshRenderer>();

        }
    }
}
