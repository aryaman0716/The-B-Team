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
    private Collider myCollider;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        myCollider = GetComponent<Collider>();
        if (myCollider == null)
            Debug.Log("ScrewInteractable on " + gameObject.name + " has no Collider component. Please add one for proper interaction.");
    }

    void Update()
    {
        if (isRemoved) return;  // Prevent interaction if already removed
        if (!ventSystem || !ventSystem.isActivated) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (equipment == null || equipment.GetCurrentIndex() != knifeIndex) return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            const float maxDistance = 5f;

            // Ignore Player layer; also ignore trigger colliders
            int mask = ~LayerMask.GetMask("Player");

            // Gather all hits so we can determine what the ray actually intersects first
            RaycastHit[] hits = Physics.RaycastAll(ray, maxDistance, mask, QueryTriggerInteraction.Ignore);
            if (hits == null || hits.Length == 0)
            {
                // no hits at all
                return;
            }

            // Find the first hit that is this screw (or a child of it)
            foreach (var hit in hits)
            {
                if (hit.collider == null) continue;

                // If the hit collider belongs to this screw, accept it
                if (hit.collider.gameObject == gameObject || hit.collider.transform.IsChildOf(transform))
                {
                    RemoveScrew();
                    return;
                }
            }
        }
    }

    void RemoveScrew()
    {
        isRemoved = true;
        Collider col = myCollider ?? GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false; // Disable collider to prevent further interactions
        }

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