using UnityEngine;
public class VentSystem : MonoBehaviour
{
    public GameObject vent;
    //public Rigidbody ventCover;
    public VentFocusController focusController;
    public bool isActivated = false;
    public bool ventOpened = false;

    private int screwsRemoved = 0;
    private int totalScrews = 4;
    public void ActivateVent()
    {
        isActivated = true;
        GetComponent<AudioSource>().volume = (0.5f * GlobalSettings.SFXVolume * GlobalSettings.MasterVolume);
        GetComponent<AudioSource>().Play();
        GetComponentInChildren<Animator>().SetTrigger("VentSystemOn");
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
        var ventCover = GameObject.FindWithTag("VentCover").GetComponent<Rigidbody>();
        ventCover.isKinematic = false;
        ventCover.AddForce(new Vector3(0, 0, 0.2f), ForceMode.Impulse);
        ventCover.useGravity = true;
        ventOpened = true;
        if (focusController != null)
        {
            focusController.ExitFocusMode();
        }
    }
}
