using UnityEngine;

public class ManagerRoomButton : MonoBehaviour
{
    private GeneralDoor doorToUnlock;
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
        outline = GetComponent<Outline>();
        mouseOver = false;
        pressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!mouseOver || pressed) { return; }
        outline.enabled = mouseOver;
        if (Input.GetMouseButtonDown(0)) 
        {
            doorToUnlock.SetDoorLocked(false);
            outline.enabled = false;
            pressed = true;
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
