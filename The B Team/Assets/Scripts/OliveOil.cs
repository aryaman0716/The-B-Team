using UnityEngine;
public class OliveOil : MonoBehaviour
{
    public float useDistance = 3f;
    public GeneralDoor door;
    //[SerializeField] private GameObject highlightEmitter;

    void Update()
    {
        if (GetComponent<PlacementEmitter>().IsPlaced)
        {
            UseOil();
        }
    }
    void UseOil()
    {
        door.SetDoorLocked(false);
        gameObject.SetActive(false);
        ObjectiveManager.Instance.CompleteObjective("Find something to oil the door.", "Find a way to unlock the shutter.");
    }

    
}
