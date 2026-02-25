using UnityEngine;

public class WireActivate : MonoBehaviour
{
    public Material offMat;
    public Material onMat;
    private MeshRenderer mr;

    void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    public void ActivateWire()
    {
        mr.material = onMat;
    }

    public void DeactivateWire()
    {
        mr.material = offMat;
    }
}