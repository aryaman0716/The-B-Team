using UnityEngine;
using TMPro;
using System.Collections;

public class MicrowaveController : MonoBehaviour
{
    public Transform doorPivot;
    public float openAngle = 90f; 
    public float smoothSpeed = 5f;

    [Header("Cooking")]
    public GameObject cookedKeyPrefab;
    public Transform spawnPoint;

    private bool isOpen = false;
    public bool open => isOpen;
    private bool keyInside = false;
    private GameObject currentKey = null;
    private bool keyCooked;
    private bool keyCooking;
    public bool KeyCooked => keyCooked;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private EquipmentController equipment;
    public GameObject Player;
    public static bool mousingM = false;
    

    void Start()
    {
        closedRotation = doorPivot.localRotation;
        openRotation = Quaternion.Euler(0, openAngle, 0);
        equipment = FindFirstObjectByType<EquipmentController>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        Quaternion targetRotation = isOpen ? openRotation : closedRotation;
        doorPivot.localRotation = Quaternion.Slerp(doorPivot.localRotation, targetRotation, Time.deltaTime * smoothSpeed);
        if (keyCooking) { return; }
        CheckInteraction();

        if (Input.GetMouseButtonDown(0) && mousingM) 
        {
            ToggleDoor();
            return;
        }

        currentKey = GameObject.FindGameObjectWithTag("MouldedKey");
        if (currentKey != null && currentKey.GetComponent<PlacementEmitter>().IsPlaced && isOpen == false)
        {
            StartCoroutine(CookKey());
        }
        if (keyCooked && isOpen && doorPivot.localRotation == openRotation)
        {
            GetComponent<MicrowaveController>().enabled = false;
        }
    }

    void CheckInteraction()
    {
        if (EquipmentController.publicIndex < 4)
        {
            return;
        }
        if (Player != null)
        {
            if (PlayerDistance() > 3f) return ;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit))
            return;

        if (!(hit.transform == this.transform || hit.transform.IsChildOf(this.transform)))
            return;

        if (equipment != null && equipment.GetCurrentIndex() != equipment.TotalTools())
        {
            Debug.Log("Can't interact with microwave while holding a tool.");
            return;
        }
        mousingM = true;
        
    }
    public void ToggleDoor()
    {
        isOpen = !isOpen;

    }
    //void CookKey()
    //{
    //    Debug.Log("Cooking the key...");
    //    if (currentKey != null)
    //    {
    //        Destroy(currentKey);
    //    }
    //    if (cookedKeyPrefab != null)
    //    {
    //        var obj = Instantiate(cookedKeyPrefab, spawnPoint.position, spawnPoint.rotation);
    //        obj.GetComponentInChildren<PlacementEmitter>().previewMeshes[0] = GameObject.Find("cookedKeyPreviewMeshSolid").GetComponent<MeshRenderer>();
    //        obj.GetComponentInChildren<PlacementEmitter>().previewHighlight = GameObject.Find("cookedKeyPreviewHighlight").GetComponent<MeshRenderer>();
    //        keyCooked = true;
    //        GetComponent<BoxCollider>().enabled = false;
    //    }
    //    keyInside = false;
    //    currentKey = null;
    //}

    IEnumerator CookKey()
    {
        if(keyCooking || keyCooked) { yield return null; }
        isOpen = false;
        keyCooking = true;
        
        yield return new WaitUntil(() => doorPivot.localRotation == closedRotation);
        
        Destroy(currentKey);
        
        yield return new WaitForSeconds(1.5f);

        var obj = Instantiate(cookedKeyPrefab, spawnPoint.position, spawnPoint.rotation);
        obj.GetComponentInChildren<PlacementEmitter>().previewMeshes[0] = GameObject.Find("cookedKeyPreviewMeshSolid").GetComponent<MeshRenderer>();
        obj.GetComponentInChildren<PlacementEmitter>().previewHighlight = GameObject.Find("cookedKeyPreviewHighlight").GetComponent<MeshRenderer>();
        
        GetComponent<BoxCollider>().enabled = false;
        isOpen = true;
        
        yield return new WaitForSeconds(1f);
        
        keyCooked = true;
        
        yield return null;

    }
    
    private float PlayerDistance()
    {
        float distance = Vector3.Distance(Player.transform.position, transform.position);
        return distance;
    }
}