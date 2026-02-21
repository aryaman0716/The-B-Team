using UnityEngine;

[CreateAssetMenu(menuName = "Tools/Flour Tool")]
public class FlourTool : ToolData
{
    public GameObject flourDustPrefab;
    public override void UseTool(Transform origin)
    {
        VentSystem ventSystem = GameObject.FindFirstObjectByType<VentSystem>();
        if (ventSystem == null)
        {
            Debug.Log("VentSystem not found!");
            return;
        }
        if (!ventSystem.ventOpened)
        {
            Debug.Log("VentSystem is not opened!");
            return;
        }
        Debug.Log("Flour used!");
        Vector3 spawnPos = origin.position + origin.forward * 0.8f;
        Instantiate(flourDustPrefab, spawnPos, origin.rotation);
    }
}
