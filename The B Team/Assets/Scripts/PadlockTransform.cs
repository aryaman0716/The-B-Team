using UnityEngine;

public class PadlockTransform : MonoBehaviour
{
    [Header("Settings")]
    public GameObject keyPrefab;    
    public Transform spawnPoint;
    public Transform shutter;
    public float openHeight = 4f;
    public float openSpeed = 2f;

    private bool doughUsed = false;
    private bool unlocking = false;
    private Vector3 shutterClosedPos;
    private Vector3 shutterOpenPos;
    private EquipmentController equipment;
    //private PlacementListener listener;
    public DialogueTrigger dia;

    void Start()
    {
        if (shutter != null)
        {
            shutterClosedPos = shutter.position;
            shutterOpenPos = shutterClosedPos + Vector3.up * openHeight;
        }
        //listener = GetComponentInChildren<PlacementListener>();
        equipment = FindFirstObjectByType<EquipmentController>();
    }
    void Update()
    {
        if (unlocking && shutter != null)
        {
            shutter.position = Vector3.Lerp(shutter.position, shutterOpenPos, Time.deltaTime * openSpeed); 
        }

    }

    public void TransformDoughToKey(GameObject dough)
    {
        dia.TriggerDialogue();
        Debug.Log("Dough touching padlock! Transforming to Key...");  
        Destroy(dough);
        if (keyPrefab != null)
        {
            Vector3 spawnPos = (spawnPoint != null) ? spawnPoint.position : transform.position;
            Quaternion spawnRot = (spawnPoint != null) ? spawnPoint.rotation : Quaternion.identity;

            GameObject newKey = Instantiate(keyPrefab, spawnPos, spawnRot);
            newKey.GetComponent<PlacementEmitter>().previewHighlight = GameObject.Find("mouldedKeyPreviewHighlight").GetComponent<MeshRenderer>();
            newKey.GetComponent<PlacementEmitter>().previewMeshes[0] = GameObject.Find("mouldedKeyPreviewMeshSolid").GetComponent<MeshRenderer>();
            Rigidbody rb = newKey.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }
            //listener.placementID = "cookedKey";
        }
    }
    public void UnlockPadlock(GameObject cookedKey)
    {
        Debug.Log("Cooked key used! Shutter is now opening...");
        Destroy(cookedKey);
        // if the player is holding the cooked key, they drop it
        //if (cookedKey != null)
        //{
        //    var pickup = cookedKey.GetComponent<Pickup>();
        //    if (pickup != null && pickup.IsHolding)
        //    {
        //        pickup.Drop();
        //    }
        //}
        ObjectiveManager.Instance.CompleteObjective("Find a way to unlock the shutter.");
        ObjectiveManager.Instance.SetObjective("Find the key for the manager's office.");
        if (equipment == null)
            equipment = FindFirstObjectByType<EquipmentController>();

        if (equipment != null)
        {
            equipment.SetCanEquip(true);
            equipment.SetHolding(false);
        }

        
        unlocking = true;
    }
}