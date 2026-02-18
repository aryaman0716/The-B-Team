using UnityEngine;

[CreateAssetMenu(menuName = "Tools/Spatula Tool")]
public class SpatulaTool : ToolData
{
    public override void UseTool(Transform origin)
    {
        Debug.Log("Spatula used!");
    }
}
