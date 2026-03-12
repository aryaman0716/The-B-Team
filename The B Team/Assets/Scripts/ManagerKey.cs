using UnityEngine;

public class ManagerKey : MonoBehaviour
{
    public float interactDistance = 3.0f;
    public bool isGrabbed = false;

    void Update()
    {
        if (isGrabbed && Input.GetMouseButtonDown(1))
        {
            
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.5f);
            foreach (var hitCollider in hitColliders)
            {
                ManagerDoor door = hitCollider.GetComponentInParent<ManagerDoor>();
                if (door != null)
                {
                    door.UnlockAndOpen();
                    Debug.Log("Unlocked using OverlapSphere!");
                    return; 
                }
            }
        }
    }

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 1.5f);
    }

    void ShootRaycast()
    {
        RaycastHit hit;
        Vector3 startPos = transform.position + (transform.forward * 0.5f);
        if (Physics.Raycast(startPos, transform.forward, out hit, interactDistance))
        {
            Debug.Log("Raycast Hit: " + hit.collider.name);

            
            ManagerDoor door = hit.collider.GetComponentInParent<ManagerDoor>();

            if (door != null)
            {
                door.UnlockAndOpen();
            }
        }
    }


}