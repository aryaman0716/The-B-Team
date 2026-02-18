using UnityEngine;

[CreateAssetMenu(menuName = "Tools/Flour Tool")]
public class FlourTool : ToolData
{
    public override void UseTool(Transform origin)
    {
        Debug.Log("Flour used!");
    }
}
