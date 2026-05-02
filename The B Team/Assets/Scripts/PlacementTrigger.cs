using UnityEngine;

public class PlacementTrigger : MonoBehaviour
{
    //Passes through collisions of child to parent emitter object so size of snap placment can be customised :3c
    private PlacementEmitter emitter;

    void Awake()
    {
        emitter = GetComponentInParent<PlacementEmitter>();
        if(emitter == null) { Debug.Log("Emitter parent not found"); enabled = false; }
    }

    void OnTriggerStay(Collider col)
    {
        emitter?.TriggerEnter(col);
    }

    void OnTriggerExit(Collider col)
    {
        emitter?.TriggerExit(col);
    }

    
}
