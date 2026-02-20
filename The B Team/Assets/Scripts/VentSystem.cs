using UnityEngine;
public class VentSystem : MonoBehaviour
{
    public GameObject vent;
    public GameObject ventCover;
    public VentFocusController focusController;
    public bool isActivated = false;

    private int screwsRemoved = 0;
    private int totalScrews = 4;
    public void ActivateVent()
    {
        isActivated = true;
        Debug.Log("Vent activated!");
    }
    public void ScrewRemoved()
    {
        screwsRemoved++;
        if (screwsRemoved >= totalScrews)
        {
            RemoveVentCover();
        }
    }
    void RemoveVentCover()
    {
        if (ventCover != null)
        {
            Rigidbody rb = ventCover.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = ventCover.AddComponent<Rigidbody>();
            }
            rb.isKinematic = false;
            rb.useGravity = true;
        }
        if (focusController != null)
        {
            focusController.ExitFocusMode();
        }
    }
}
