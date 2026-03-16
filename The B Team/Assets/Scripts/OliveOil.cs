using UnityEngine;
public class OliveOil : MonoBehaviour
{
    public float useDistance = 3f;
    public Room1ExitDoor exitDoor;
    //[SerializeField] private GameObject highlightEmitter;

    void Update()
    {
        if (GetComponentInChildren<PlacementHighlight>().isPlaced)
        {
            UseOil();
        }
    }
    void UseOil()
    {
        exitDoor.ApplyOil();
        gameObject.SetActive(false);
    }

    
}
