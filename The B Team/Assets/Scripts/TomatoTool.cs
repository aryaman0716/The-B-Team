using UnityEngine;

[CreateAssetMenu(menuName = "Tools/Tomato Tool")]
public class TomatoTool : ToolData
{
    public GameObject projectilePrefab; 
    public float throwForce = 20f;
    public Animator animator;

    public override void UseTool(Transform origin)
    {
        animator = GameObject.FindGameObjectWithTag("Tool").GetComponent<Animator>();
        animator.SetTrigger("Throw");


        if (projectilePrefab == null) return;

        Vector3 spawnPos = origin.position + (origin.forward * 1.0f);

        GameObject tomato = Instantiate(projectilePrefab, spawnPos, origin.rotation);
        Rigidbody rb = tomato.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(origin.forward * throwForce, ForceMode.VelocityChange);
        }

        
    }
}