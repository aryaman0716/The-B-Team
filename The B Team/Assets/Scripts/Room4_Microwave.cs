using UnityEngine;

public class Room4_Microwave : MonoBehaviour
{
    
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private PlacementEmitter emitter;
    //[SerializeField] private PlacementHighlight cutleryPlacementEmitter;

    [SerializeField] private bool doorOpened = false;
    public bool DoorOpened => doorOpened;
    public bool cutleryPlaced = false;
    //[SerializeField] private bool doorClosed = false;
    [SerializeField] private bool primed = false;
    public bool Primed => primed;
    private bool openable = false;
    public bool Openable => openable;
    [SerializeField] private EquipmentController equipment;
    [SerializeField] private GameObject cutleryObj;

    public static bool mousing;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        emitter = GetComponent<PlacementEmitter>();
        equipment = FindFirstObjectByType<EquipmentController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (emitter.IsPlaced && openable == false)
        {
            openable = true;
            emitter.enabled = false;
            doorAnimator.enabled = true;
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
        if(mousing == false) mousing = true; 
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

    void OnMouseEnter()
    {
        mousing = true;
    }
    void OnMouseExit()
    {
        mousing = false;
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
