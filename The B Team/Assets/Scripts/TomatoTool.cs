using UnityEngine;

[CreateAssetMenu(menuName = "Tools/Tomato Tool")]
public class TomatoTool : ToolData
{
    public GameObject projectilePrefab;
    public float throwForce = 20f;
    public override void UseTool(Transform origin)
    {
        //Debug.Log("Tomato thrown!");
        if (projectilePrefab == null || origin == null) return;
        GameObject tomato = Instantiate(projectilePrefab, origin.position + origin.forward, origin.rotation);
        Rigidbody rb = tomato.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(origin.forward * throwForce, ForceMode.VelocityChange);
        }
    }
}
