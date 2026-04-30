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
    public bool FlourAdded => flourAdded;
    private bool doughCreated = false;

    void Start()
    {
        flour_obj = GameObject.Find("flour_obj");
        flour_obj.SetActive(false);
    }

    void Update()
    {
        if (!flourAdded) { return; }
        var distanceToPlayer = Vector3.Distance(transform.position, GameObject.Find("ChefPlayer").transform.position);
        if(distanceToPlayer < 4 && Input.GetMouseButtonDown(0) && SinkInteractable.mousingS && EquipmentController.publicIndex == 4)
        {
            KneadDough();
        }
    }
    public void AddFlour()
    {
        if (!sinkInteractable.Filled)
        {
            Debug.Log("Needs more water");
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

    }
    public void KneadDough()
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
        GetComponent<Collider>().enabled = false;
        flour_obj.SetActive(false);
        GameObject.Find("water_obj").SetActive(false);
        doughCreated = true;
        Debug.Log("Dough created!");
        if (doughPrefab != null && doughSpawnPoint != null)
        {
            GameObject obj = Instantiate(doughPrefab, doughSpawnPoint.position, Quaternion.identity);
            obj.GetComponent<PlacementEmitter>().previewMeshes[0] = GameObject.Find("doughPreviewMeshSolid").GetComponent<MeshRenderer>();
            obj.GetComponent<PlacementEmitter>().previewHighlight = GameObject.Find("doughPreviewHighlight").GetComponent<MeshRenderer>();

        }
    }
}
