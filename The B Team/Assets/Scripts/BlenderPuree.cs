using UnityEngine;
using TMPro;

public class BlenderPuree : MonoBehaviour
{
    public GameObject pureePuddlePrefab;
    public LayerMask pureeLayer;
    public float dropInterval = 0.5f;

    private bool isFilled = false;
    private bool isHeld = false;
    private float timer; 
    private Pickup pickup;
    public GameObject sauce;
    private void Start()
    {
        pickup = GetComponent<Pickup>();
        GetComponent<Rigidbody>().isKinematic = true;
        // The blender cannot be picked up until it is filled with puree.
        if (pickup != null)
        {
            pickup.enabled = false;
        }
    }
    void Update()
    {
        if (!isFilled || !isHeld) return; 
        timer += Time.deltaTime;
        if (timer >= dropInterval)
        {
            timer = 0f;
            SpawnPuddle();
        }
    }
    void SpawnPuddle()
    {
        if (pureePuddlePrefab == null) return;

        // make the puree puddle spawn and fall to the ground below the blender
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 10f, 1 << LayerMask.NameToLayer("Ground")))
        {
            Vector3 spawnPosition = hit.point + Vector3.up * 0.05f; 
            Instantiate(pureePuddlePrefab, spawnPosition, Quaternion.identity);
        }
    }
    public void FillBlender()
    {
        if (isFilled) return;
        sauce.SetActive(true);
        GetComponent<Rigidbody>().isKinematic = false;

        if (GetComponentInChildren<TMP_Text>() != null)
        {
            GetComponentInChildren<TMP_Text>().text = "Filled Blender";
        }
        
        isFilled = true;
        Debug.Log("Blender filled with tomato puree!");
        if (pickup != null)
        {
            pickup.enabled = true;
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("TomatoProjectile") && !isFilled)
        {
            FillBlender();
        }
    }
    public void SetHeld(bool state)
    {
        isHeld = state;
    } 
}

