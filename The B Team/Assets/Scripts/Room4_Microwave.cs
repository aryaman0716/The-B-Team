using UnityEngine;

public class Room4_Microwave : MonoBehaviour
{
    
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private PlacementHighlight ph;
    //[SerializeField] private PlacementHighlight cutleryPlacementEmitter;

    [SerializeField] private bool doorOpened = false;
    public bool DoorOpened => doorOpened;
    [SerializeField] private bool cutleryPlaced = false;
    //[SerializeField] private bool doorClosed = false;
    [SerializeField] private bool primed = false;
    public bool Primed => primed;
    private bool openable = false;
    [SerializeField] private EquipmentController equipment;
    [SerializeField] private GameObject cutleryObj;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ph.isPlaced && openable == false)
        {
            openable = true;
            doorAnimator.enabled = true;
        }

        if(cutleryObj.GetComponentInChildren<PlacementHighlight>().isPlaced == true)
        {
            cutleryPlaced = true;
        }

        
        
    }

    void LateUpdate()
    {
        if (cutleryPlaced && !doorOpened)
        {
            openable = false;
            primed = true;
        }
    }

    void OnMouseOver()
    {
        if (!openable) {  return; }

        if (Input.GetMouseButtonDown(0))
        {
            if (equipment != null && equipment.GetCurrentIndex() != equipment.TotalTools())
            {
                Debug.Log("Can't use microwave while holding a tool.");
                return;
            }

            if (doorOpened)
            {
                ToggleDoor(false);
                return;
            }
            else
            {
                ToggleDoor(true);
                return;
            }
        }
    }
    public void ToggleDoor(bool val)
    {
        doorAnimator.SetBool("DoorState", val);
        doorOpened = val;
    }

    public void PlaceCutlery()
    {

    }
}
