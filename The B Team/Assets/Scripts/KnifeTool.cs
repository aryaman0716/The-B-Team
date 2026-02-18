using UnityEngine;

[CreateAssetMenu(menuName = "Tools/Knife Tool")]
public class KnifeTool : ToolData
{
    public override void UseTool(Transform origin)
    {
        Debug.Log("Knife used!");
    }
}
