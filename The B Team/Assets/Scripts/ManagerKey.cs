using UnityEngine;

public class ManagerKey : MonoBehaviour
{
    public float interactDistance = 3.0f;
    public bool isGrabbed = false;

    private GeneralDoor door;

    void Start()
    {
        door = GameObject.Find("ManagerDoor").GetComponent<GeneralDoor>();
    }

    void Update()
    {
        if (GetComponent<PlacementEmitter>().IsPlaced)
        {
            UseKey();
        }
        //if (isGrabbed && Input.GetMouseButtonDown(1))
        //{
            
        //    Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.5f);
        //    foreach (var hitCollider in hitColliders)
        //    {
        //        ManagerDoor door = hitCollider.GetComponentInParent<ManagerDoor>();
        //        if (door != null)
        //        {
        //            door.UnlockAndOpen();
        //            Debug.Log("Unlocked using OverlapSphere!");
        //            return; 
        //        }
        //    }
        //}
    }

    void UseKey()
    {
        door.SetDoorLocked(false);
        gameObject.SetActive(false);
    }
}