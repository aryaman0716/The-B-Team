using UnityEngine;

public class PlacementEmitter : MonoBehaviour
{
    public string placementID;
    public bool isActive = true;

    private PlacementListener currentListener;
    private Pickup pickup;
    private Rigidbody rb;
    public Collider placementTrigger;
    public MeshRenderer previewHighlight;
    public MeshRenderer[] previewMeshes;
    public MeshRenderer[] meshesToHide;

    private bool isPlaced = false;
    public bool IsPlaced => isPlaced;


    void Start()
    {
        pickup = GetComponent<Pickup>();
        rb = GetComponent<Rigidbody>();
        if(meshesToHide == null)
        {
            Debug.Log("Meshes to hide not assigned");
        }

        previewHighlight.enabled = false;
        EnablePreviewMeshes(false);
        
        placementTrigger = GetComponentInChildren<Collider>(true);

        if (placementTrigger == null || !placementTrigger.isTrigger)
        {
            Debug.Log("No trigger found or collider not set to trigger");
        }
    }

    void Update()
    {
        if (!isActive || isPlaced || pickup == null) { return; }

        if (pickup.IsHolding)
        {
            EnablePreviewHighlight(true);
        }
        else
        {
            EnablePreviewHighlight(false);
        }

        if (currentListener == null) { return; }

        if (!pickup.IsHolding)
        {
            PlaceObject();
        }//Only place object when its not picked up and currentlistener is not null (means youre in range of a listener with a corresponding id)
        
    }

    void PlaceObject()
    {
        if (currentListener == null) { return; }

        isPlaced = true;

        //Set position + rotation
        Transform t = currentListener.snapPoint;
        transform.position = t.position;
        transform.rotation = t.rotation;

        //Lock rigidbody
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        //Disable relevant components
        placementTrigger.enabled = false;
        pickup.enabled = false;
        EnablePreviewMeshes(false);
        EnablePreviewHighlight(false);
    }

    private void EnablePreviewMeshes(bool val)
    {
        if(previewMeshes == null || meshesToHide == null) { return; }

        HideOwnMeshes(val);

        for (int i = 0; i < previewMeshes.Length; i++)
        {
            previewMeshes[i].enabled = val;
        }


    }

    private void EnablePreviewHighlight(bool val)
    {
        if(previewHighlight == null) { return; }

        Debug.Log("Object picked up, highlighter enabled is " + val);
        previewHighlight.enabled = val;
    }

    private void HideOwnMeshes(bool val)
    {
        if(meshesToHide == null) { return; }

        for (int i = 0; i < meshesToHide.Length; i++) 
        { 
            meshesToHide[i].enabled = !val;
        }
    }

    public void TriggerEnter(Collider col)//called from placementTrigger child
    {
        if (isPlaced) { return; }
        Debug.Log("Emitter entering listener");
        PlacementListener listener = col.GetComponent<PlacementListener>();

        if (listener == null) { Debug.Log("Listener not found"); return; }
        if(listener.placementID != placementID) { Debug.Log("Listener ID not matching"); return; }

        currentListener = listener;
        EnablePreviewMeshes(true);
    }

    public void TriggerExit(Collider col)//also called from placementTrigger child
    {
        if (isPlaced) { return; }
        Debug.Log("Emitter exiting listener");
        PlacementListener listener = col.GetComponent<PlacementListener>();
        if (listener == null || listener.placementID != placementID) { return; }

        if (currentListener == listener) { currentListener = null; }
        EnablePreviewMeshes(false);
    }
}
