using UnityEngine;

public class WireController : MonoBehaviour
{
    public GameObject[] wires;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateWires()
    {
        foreach (GameObject wire in wires)
        {
            wire.GetComponent<WireActivate>().ActivateWire();
        }
    }
}
