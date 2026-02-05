using UnityEngine;

[CreateAssetMenu(menuName = "Tools")]
public class ToolData : ScriptableObject
{
    public string toolName;
    public Sprite toolIcon;
    public GameObject toolPrefab;
}
