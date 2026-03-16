using UnityEngine;

[CreateAssetMenu(menuName = "Tools/Flour Tool")]
public class FlourTool : ToolData
{
    public GameObject flourDustPrefab;
    public float maxUseDistance = 5f;

    public override void UseTool(Transform origin)
    {
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
            SinkMixSystem sink = hit.collider.GetComponentInParent<SinkMixSystem>();
            if (sink != null)
            {
                SpawnParticle(hit);
                sink.AddFlour();
                return;
            }
        }
        VentSystem ventSystem = GameObject.FindFirstObjectByType<VentSystem>();
        if (ventSystem == null)
        {
            Debug.Log("VentSystem not found!");
            return;
        }
        //if (!ventSystem.ventOpened)
        //{
        //    Debug.Log("VentSystem is not opened!");
        //    return;
        //}
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
