using UnityEngine;

public class ManagerRoomButton : MonoBehaviour
{
    private GeneralDoor doorToUnlock;
    private WireActivate wireToActivate;
    private Outline outline;
    private Transform playerTransform;
    public float interactRange;

    public bool mouseOver;
    public bool pressed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        doorToUnlock = GameObject.Find("Room3AccessDoor").GetComponent<GeneralDoor>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        wireToActivate = GameObject.Find("ManagerRoomWire").GetComponent<WireActivate>();
        outline = GetComponent<Outline>();
        mouseOver = false;
        pressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pressed) { return; }
        outline.enabled = mouseOver;
        if (mouseOver && Input.GetMouseButtonDown(0)) 
        {
            doorToUnlock.SetDoorLocked(false);
            wireToActivate.ActivateWire();
            outline.enabled = false;
            pressed = true;
            MusicManger.phase2 = true;
        }

    }

    void OnMouseOver()
    {
        var dist = Vector3.Distance(playerTransform.position, transform.position);
        if (dist > interactRange) { mouseOver = false; return; }
        mouseOver = true;
    }

    void OnMouseExit()
    {
        mouseOver = false;
    }
}
