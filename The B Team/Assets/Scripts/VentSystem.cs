using UnityEngine;
public class VentSystem : MonoBehaviour
{
    public GameObject vent;
    public GameObject ventCover;
    public VentFocusController focusController;
    public bool isActivated = false;
    public MeshRenderer ventRenderer;
    private int screwsRemoved = 0;
    private int totalScrews = 4;
    public void ActivateVent()
    {
        isActivated = true;
       
        Renderer renderer = ventCover.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.green;
        }
        Debug.Log("Vent activated and turned green!");
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
            ventCover.GetComponent<Rigidbody>().isKinematic = false;
            ventCover.GetComponent<Rigidbody>().useGravity = true;
        }
        if (focusController != null)
        {
            focusController.ExitFocusMode();
        }
    }


}
