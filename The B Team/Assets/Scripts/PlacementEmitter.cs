using UnityEngine;

public class PlacementEmitter : MonoBehaviour
{
    public string placementID;

    private PlacementListener currentListener;
    private Pickup pickup;
    private Rigidbody rb;
    public Collider placementTrigger;
    public MeshRenderer previewHighlight;
    public MeshRenderer[] previewMeshes;
    private MeshRenderer ownMesh;

    private bool isPlaced = false;
    public bool IsPlaced => isPlaced;


    void Start()
    {
        pickup = GetComponent<Pickup>();
        rb = GetComponent<Rigidbody>();
        ownMesh = GetComponent<MeshRenderer>();
        EnablePreviewMeshes(false);
        
        placementTrigger = GetComponentInChildren<Collider>(true);

        if (placementTrigger == null || !placementTrigger.isTrigger)
        {
            Debug.Log("No trigger found or collider not set to trigger");
        }
    }

    void Update()
    {
        if (isPlaced || pickup == null) { return; }

        //if (pickup.IsHolding)
        //{
        //    EnablePreviewHighlight(true);
        //}
        //else
        //{
        //    EnablePreviewHighlight(false);
        //}

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
        if(previewMeshes == null || ownMesh == null) { return; }

        ownMesh.enabled = !val;
        Debug.Log("Own mesh visible: " + ownMesh);

        for (int i = 0; i < previewMeshes.Length; i++)
        {
            previewMeshes[i].enabled = val;
        }


    }

    private void EnablePreviewHighlight(bool val)
    {
        if(previewHighlight == null) { return; }

        previewHighlight.enabled = val;
    }

    public void TriggerEnter(Collider col)//called from placementTrigger child
    {
        if (isPlaced) { return; }

        PlacementListener listener = col.GetComponent<PlacementListener>();
        if (listener == null || listener.placementID != placementID) { return; }

        currentListener = listener;
        EnablePreviewMeshes(true);
    }

    public void TriggerExit(Collider col)//also called from placementTrigger child
    {
        if (isPlaced) { return; }

        PlacementListener listener = col.GetComponent<PlacementListener>();
        if (listener == null || listener.placementID != placementID) { return; }

        if (currentListener == listener) { currentListener = null; }
        EnablePreviewMeshes(false);
    }
}
