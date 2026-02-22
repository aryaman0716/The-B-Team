using UnityEngine;

[CreateAssetMenu(menuName = "Tools/Spatula Tool")]
public class SpatulaTool : ToolData
{
    public override void UseTool(Transform origin)
    {
        Debug.Log("Spatula used!");
        Animator anim = origin.GetComponentInChildren<Animator>();
        if (anim != null)
        {
            anim.SetTrigger("Use");
        }
    }
}
