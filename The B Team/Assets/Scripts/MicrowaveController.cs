using UnityEngine;

public class MicrowaveController : MonoBehaviour
{
    public Transform doorPivot;
    public float openAngle = -90f; 
    public float smoothSpeed = 5f; 
    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        
        closedRotation = doorPivot.localRotation;
        
        openRotation = Quaternion.Euler(0, openAngle, 0);
    }

    void Update()
    {
        
        Quaternion targetRotation = isOpen ? openRotation : closedRotation;
        doorPivot.localRotation = Quaternion.Slerp(doorPivot.localRotation, targetRotation, Time.deltaTime * smoothSpeed);

        
        if (Input.GetMouseButtonDown(1)) 
        {
            CheckInteraction();
        }
    }

    void CheckInteraction()
    {
        //Raycast 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if we clicked on this microwave.
            if (hit.transform == this.transform || hit.transform.IsChildOf(this.transform))
            {
                // Let's assume "Empty Hand" is a check for a certain condition. 
                // Here, we will immediately toggle the state on/off.
                ToggleDoor();
            }
        }
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;
        Debug.Log(isOpen ? "Microwave opened" : "Microwave closed");
    }
}