using UnityEngine;

public class PadlockTransform : MonoBehaviour
{
    [Header("Settings")]
    public GameObject keyPrefab;    
    public Transform spawnPoint;
    public Transform shutter;
    public float openHeight = 4f;
    public float openSpeed = 2f;

    private bool doughUsed = false;
    private bool unlocking = false;
    private Vector3 shutterClosedPos;
    private Vector3 shutterOpenPos;
    void Start()
    {
        if (shutter != null)
        {
            shutterClosedPos = shutter.position;
            shutterOpenPos = shutterClosedPos + Vector3.up * openHeight;
        }
    }
    void Update()
    {
        if (unlocking && shutter != null)
        {
            shutter.position = Vector3.Lerp(shutter.position, shutterOpenPos, Time.deltaTime * openSpeed); 
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!doughUsed && other.CompareTag("Dough"))
        {
            TransformDoughToKey(other.gameObject);
            doughUsed = true;
        }
        if (other.CompareTag("CookedKey") && doughUsed)
        {
            UnlockPadlock(other.gameObject);
        }
    }
    void TransformDoughToKey(GameObject dough)
    {
        Debug.Log("Dough touching padlock! Transforming to Key...");  
        Destroy(dough);
        if (keyPrefab != null)
        {
            Vector3 spawnPos = (spawnPoint != null) ? spawnPoint.position : transform.position;
            Quaternion spawnRot = (spawnPoint != null) ? spawnPoint.rotation : Quaternion.identity;

            GameObject newKey = Instantiate(keyPrefab, spawnPos, spawnRot);
            Rigidbody rb = newKey.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }
        }
    }
    void UnlockPadlock(GameObject cookedKey)
    {
        Debug.Log("Cooked key used! Shutter is now opening...");
        Destroy(cookedKey);
        unlocking = true;
    }
}