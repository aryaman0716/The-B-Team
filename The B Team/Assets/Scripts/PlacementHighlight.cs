using UnityEngine;

public class PlacementHighlight : MonoBehaviour
{
    [SerializeField] private GameObject objectHighlight;
    [SerializeField] private MeshRenderer parent_mr;
    [SerializeField] private GameObject listener;

    void Start()
    { 
        if(objectHighlight == null)
        {
            GetComponent<PlacementHighlight>().enabled = false; return;
        }
        objectHighlight.SetActive(false);

    }

    void ActivateHighlight()
    {
        if (objectHighlight == null)
        {
            Debug.Log("Placement highlight object null");
            return;
        }
        Debug.Log("Highlight activated");
        objectHighlight.SetActive(true);
        parent_mr.enabled = false;
    }

    void DeactivateHighlight()
    {
        if (objectHighlight == null)
        {
            Debug.Log("Placement highlight object null");
            return;
        }
        Debug.Log("Highlight deactivated");

        objectHighlight.SetActive(false);
        parent_mr.enabled = true;
    }

    void OnTriggerEnter(Collider col)
    {
        if (Input.GetMouseButton(0))
        {
            ActivateHighlight();
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (!Input.GetMouseButton(0))
        {
            DeactivateHighlight();
            return;
        }
        else if(objectHighlight.activeSelf == false)
        {
            ActivateHighlight();
        }
    }
    void OnTriggerExit(Collider col)
    {
        DeactivateHighlight();
        
    }

    void OnDestroy()
    {
        GetComponent<SphereCollider>().enabled = false;
        DeactivateHighlight();
    }
}
