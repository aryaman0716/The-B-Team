using UnityEngine;
using System.Collections;
public class ScrewInteractable : MonoBehaviour
{
    public VentSystem ventSystem;
    public AudioSource unscrewSound;
    public EquipmentController equipment;
    public int knifeIndex = 0;

    private bool isRemoved = false;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }
    void OnMouseOver()
    {
        Debug.Log("Mouse over screw");

        if (isRemoved) return;  // Prevent interaction if already removed
        if (!ventSystem.isActivated) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (equipment.GetCurrentIndex() == knifeIndex)
            {
                RemoveScrew();
            }
        }
    }
    void RemoveScrew()
    {
        isRemoved = true;
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.isKinematic = false;
        rb.useGravity = true;

        ventSystem.ScrewRemoved();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (unscrewSound != null && isRemoved)
        {
            audioSource.volume = (0.5f * GlobalSettings.SFXVolume * GlobalSettings.MasterVolume);
            audioSource.PlayOneShot(unscrewSound.clip);
        }
    }
}