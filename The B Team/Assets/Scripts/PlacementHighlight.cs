using UnityEngine;

public class PlacementHighlight : MonoBehaviour
{
    //Highlight is the green highlight that shows when object is held, placement is "placed" object visual
    [SerializeField] private Pickup pickupScript;//Parent object pickup script
    [SerializeField] private GameObject highlight;//Green highlight
    [SerializeField] private GameObject placedObject;//Copy of placed object's mesh
    [SerializeField] private MeshRenderer[] meshesToHide;//Meshes on held object to hide while showing placedObject
    [SerializeField] private GameObject listener;//Listener gameobject to detect when in range to show placedObject

    [SerializeField] public bool active = true;

    public bool isPlaced = false;//Flag for if object has been dropped inside placement area and placed there
    public bool inPlace = false;//If object is in placement area, still held

    void Start()
    { 
        if(highlight == null)
        {
            GetComponent<PlacementHighlight>().enabled = false; return;
        }
        highlight.SetActive(false);
        placedObject.SetActive(false);

    }

    void Update()
    {
        if (!active) { return; }
        if (isPlaced == true) { OnDestroy(); }
        if (pickupScript.IsHolding)
        {
            ShowHighlight(true);
        }
        else
        {
            ShowHighlight(false);
        }
    }

    void ShowHighlight(bool val)
    {
        if (highlight == null) { return; }
        highlight.SetActive(val);
    }

    void ShowPlaced(bool val)
    {
        if (placedObject == null) { return; } 
        HideMeshes(val);
        placedObject.SetActive(val);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag != gameObject.tag) { return; }
        inPlace = true;
        if (Input.GetMouseButton(0))
        {
            ShowPlaced(true);
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag != gameObject.tag) { return; }
        if (!Input.GetMouseButton(0))
        {
            ShowPlaced(false);
            return;
        }
        else
        {
            ShowPlaced(true);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag != gameObject.tag) { return; }
        inPlace = false;
        ShowPlaced(false);
    }

    public void OnDestroy()
    {
        GetComponent<SphereCollider>().enabled = false;
        ShowHighlight(false);
        ShowPlaced(false);
        GetComponent<PlacementHighlight>().enabled = false;
    }

    void HideMeshes(bool val)
    {
        for(int i = 0; i < meshesToHide.Length; i++)
        {
            meshesToHide[i].enabled = !val;
        }
    }

    public Transform GetPlacementTransform()
    {
        return placedObject.transform; 
    }
}
