using UnityEngine;

public class PadlockTransform : MonoBehaviour
{
    [Header("Settings")]
    public GameObject keyPrefab;    
    public Transform spawnPoint;    

    private bool isActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        
        if (isActivated || !other.CompareTag("Dough")) return;

        isActivated = true;
        TransformDoughToKey(other.gameObject);
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
}