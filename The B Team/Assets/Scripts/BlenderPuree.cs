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
        Vector3 spawnPos = transform.position;
        spawnPos.y -= 0.5f;  
        Instantiate(pureePuddlePrefab, spawnPos, Quaternion.identity);
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

