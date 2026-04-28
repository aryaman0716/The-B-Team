using UnityEngine;

[CreateAssetMenu(menuName = "Tools/Knife Tool")]
public class KnifeTool : ToolData
{
    public float useDistance = 3f;
    public override void UseTool(Transform origin)
    {
        Debug.Log("Knife used!");
        EquipmentController equipment = FindFirstObjectByType<EquipmentController>();
        if (equipment == null) return;

        Animator anim = origin.GetComponentInChildren<Animator>();
        if (anim != null)
        {
            anim.SetTrigger("Slash");
        }

        Camera cam = Camera.main;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, useDistance))
        {
            cameraWire wire = hit.collider.GetComponent<cameraWire>();
            if (wire != null)
            {
                 wire.Cut();
                wire = null;
            }
        }
    }
}
