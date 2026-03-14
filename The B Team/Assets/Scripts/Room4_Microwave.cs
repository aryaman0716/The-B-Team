using UnityEngine;

public class Room4_Microwave : MonoBehaviour
{
    
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private PlacementHighlight ph;
    //[SerializeField] private PlacementHighlight cutleryPlacementEmitter;

    [SerializeField] private bool doorOpened = false;
    public bool DoorOpened => doorOpened;
    [SerializeField] private bool cutleryPlaced = false;
    [SerializeField] private bool doorClosed = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ph.isPlaced)
        {
            doorAnimator.SetTrigger("OpenDoor");
            doorOpened = true;
            return;
        }
    }

    public void OpenDoor()
    {

    }

    public void PlaceCutlery()
    {

    }

    public void CloseDoor()
    {

    }
}
