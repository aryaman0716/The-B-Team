using UnityEngine;

[CreateAssetMenu(menuName = "Tools/Knife Tool")]
public class KnifeTool : ToolData
{

    public Animator animator;
    public override void UseTool(Transform origin)
    {
        Debug.Log("Knife used!");
        animator = GameObject.FindGameObjectWithTag("Tool").GetComponent<Animator>();
        animator.SetTrigger("Slash");
    }
}
