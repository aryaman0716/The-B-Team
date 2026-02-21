using UnityEngine;

[CreateAssetMenu(menuName = "Tools/Knife Tool")]
public class KnifeTool : ToolData
{
    public override void UseTool(Transform origin)
    {
        Debug.Log("Knife used!");
        EquipmentController equipment = FindFirstObjectByType<EquipmentController>();
        if (equipment == null) return;

        Animator anim = origin.GetComponentInChildren<Animator>();
        if (anim != null)
        {
            anim.SetTrigger("Slash");
        }
    }
}
