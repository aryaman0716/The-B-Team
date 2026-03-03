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

        Ray ray = new Ray(origin.position, origin.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 3f))
        {
            SinkMixSystem sink = hit.collider.GetComponent<SinkMixSystem>();
            if (sink != null)
            {
                sink.MixWithSpatula();
                return;
            }
        }
    }
}
