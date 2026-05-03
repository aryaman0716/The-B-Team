using UnityEngine;
using UnityEngine.Events;

public class WireActivate : MonoBehaviour
{
    public Material offMat;
    public Material onMat;
    private MeshRenderer mr;

    public bool activated;

    void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    public void ActivateWire()
    {
        Debug.Log("Activating " + gameObject.name);
        mr.material = onMat;
    }

    public void DeactivateWire()
    {
        Debug.Log("Dectivating " + gameObject.name);
        mr.material = offMat;
    }
}