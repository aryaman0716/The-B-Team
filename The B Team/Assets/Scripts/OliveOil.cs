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
    }

    
}
