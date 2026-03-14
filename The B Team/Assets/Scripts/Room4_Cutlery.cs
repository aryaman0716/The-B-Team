using UnityEngine;

public class Room4_Cutlery : MonoBehaviour
{
    [SerializeField] private GameObject highlightEmitter;

    [SerializeField] private GameObject microwaveObj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (microwaveObj == null) { return; }

        if (microwaveObj.GetComponent<Room4_Microwave>().DoorOpened)
        {
            highlightEmitter.GetComponent<PlacementHighlight>().active = true;
        }

    }
}
