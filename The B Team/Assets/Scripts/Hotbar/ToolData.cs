using UnityEngine;

[CreateAssetMenu(menuName = "Tools")]
public abstract class ToolData : ScriptableObject
{
    public string toolName;
    public Sprite toolIcon;
    public GameObject toolPrefab;

    public abstract void UseTool(Transform origin);
}
