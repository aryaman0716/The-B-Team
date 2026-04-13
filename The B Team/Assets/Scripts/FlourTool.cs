using UnityEngine;

[CreateAssetMenu(menuName = "Tools/Flour Tool")]
public class FlourTool : ToolData
{
    public GameObject flourDustPrefab;
    public float maxUseDistance = 5f;
    public override void UseTool(Transform origin)
    {
        Animator anim = origin.GetComponentInChildren<Animator>();
        if (anim != null)
        {
            anim.SetTrigger("toss");
        }
        Camera cam = Camera.main;
        if (cam == null)
        {
            Debug.LogWarning("FlourTool: Camera.main is null.");
            return;
        }

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxUseDistance, ~0, QueryTriggerInteraction.Ignore))
        {
            SpawnParticle(hit);

        }

        Debug.Log("Flour used!");
        Vector3 spawnPos = origin.position + origin.forward * 0.8f;
        Instantiate(flourDustPrefab, spawnPos, origin.rotation);
    }
    private void SpawnParticle(RaycastHit hit)
    {
        if (flourDustPrefab == null) return;
        Quaternion rot = Quaternion.LookRotation(hit.normal);
        Instantiate(flourDustPrefab, hit.point + hit.normal * 0.01f, rot);
    }
}
