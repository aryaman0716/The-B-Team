using UnityEngine;
public class BlenderPuree : MonoBehaviour
{
    public GameObject pureePuddlePrefab;
    public float dropInterval = 0.5f;

    private bool isFilled = false;
    private bool isHeld = false;
    private float timer; 
    private Pickup pickup;
    private void Start()
    {
        pickup = GetComponent<Pickup>();
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
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 10f))
        {
            Vector3 spawnPosition = hit.point + Vector3.up * 0.1f; 
            Instantiate(pureePuddlePrefab, spawnPosition, Quaternion.identity);
        }
    }
    public void FillBlender()
    {
        if (isFilled) return;
        isFilled = true;
        Debug.Log("Blender filled with tomato puree!");
        if (pickup != null)
        {
            pickup.enabled = true;
        }
    }
    public void SetHeld(bool state)
    {
        isHeld = state;
    } 
}

