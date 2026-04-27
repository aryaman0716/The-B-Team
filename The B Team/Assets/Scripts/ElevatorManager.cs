using UnityEngine;

public partial class ElevatorManager : MonoBehaviour
{
    public Animator doorAnimator;

    
    public void UnlockElevator()
    {
        if (doorAnimator != null)
        {
            
            doorAnimator.SetTrigger("OpenDoor");
            Debug.Log("Elevator: Door Opening...");
        }
    }
}