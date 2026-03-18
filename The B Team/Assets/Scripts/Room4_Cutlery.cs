using UnityEngine;

public class Room4_Cutlery : MonoBehaviour
{
    [SerializeField] private PlacementEmitter emitter;

    [SerializeField] private GameObject microwaveObj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        emitter = GetComponent<PlacementEmitter>();
        microwaveObj = GameObject.Find("Room4_Microwave");
    }

    // Update is called once per frame
    void Update()
    {
        if (microwaveObj == null) { return; }

        if (microwaveObj.GetComponent<Room4_Microwave>().DoorOpened)
        {
            emitter.isActive = true;
        }
        else
        {
            emitter.isActive = false;
        }

        if (emitter.IsPlaced)
        {
            microwaveObj.GetComponent<Room4_Microwave>().cutleryPlaced = true;
            emitter.enabled = false;
        }
    }
}
