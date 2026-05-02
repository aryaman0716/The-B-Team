using UnityEngine;
using System.Collections;
public class ScrewInteractable : MonoBehaviour
{
    public VentSystem ventSystem;
    public AudioClip[] unscrewSound;
    public AudioClip[] hitGroundSound;
    public EquipmentController equipment;
    public int knifeIndex = 0;

    private bool isRemoved = false;
    private bool onGround = false;
    private AudioSource audioSource;
    private Collider myCollider;

    public static bool mousing = false;
    public static ScrewInteractable instance;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        myCollider = GetComponent<Collider>();
        if (myCollider == null)
            Debug.Log("ScrewInteractable on " + gameObject.name + " has no Collider component. Please add one for proper interaction.");
    }

    void Update()
    {

        if (isRemoved) return;  // Prevent interaction if already removed
        //if (!ventSystem || !ventSystem.isActivated) return;
        if (instance == this) { GetComponent<Outline>().enabled = mousing; }
        if (Input.GetMouseButtonDown(0))
        {
            if (equipment == null || equipment.GetCurrentIndex() != knifeIndex) return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            const float maxDistance = 4f;

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
        if (isRemoved) { return; }
        isRemoved = true;
        audioSource.volume = (0.5f * GlobalSettings.SFXVolume * GlobalSettings.MasterVolume);
        audioSource.PlayOneShot(unscrewSound[Random.Range(0, unscrewSound.Length)]);
        GetComponent<Outline>().enabled = false;
        mousing = false;
        Collider col = myCollider ?? GetComponent<Collider>();
        //if (col != null)
        //{
        //    col.enabled = false; // Disable collider to prevent further interactions
        //}

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;

        ventSystem.ScrewRemoved();
    }

    void OnMouseOver()
    {
        if (UIController.Paused) { GetComponent<Outline>().enabled = false; mousing = false; }
        if (isRemoved) { mousing = false; return; }
        if (instance != this) { instance = this; }
        if (EquipmentController.DistanceToPlayer(transform) > 4f)
        {
            mousing = false;
            return;
        }
        else
        {
            GetComponent<Outline>().enabled = true;
            mousing = true;
        }
    }

    void OnMouseExit()
    {
        mousing = false;
        GetComponent<Outline>().enabled = false;
        instance = null;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground" && !onGround   )
        {
            onGround = true;
            audioSource.volume = (0.5f * GlobalSettings.SFXVolume * GlobalSettings.MasterVolume);
            audioSource.PlayOneShot(hitGroundSound[Random.Range(0, hitGroundSound.Length)]);
        }
    }
}