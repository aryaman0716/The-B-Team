using UnityEngine;

public class PlacementListener : MonoBehaviour
{
    public string placementID;

    public Transform snapPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (snapPoint == null) { snapPoint = GetComponent<Transform>(); }//Defaults to own transform if no transform set in inspector
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
