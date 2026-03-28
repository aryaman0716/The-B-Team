using UnityEngine;
using TMPro;

public class MicrowaveController : MonoBehaviour
{
    public Transform doorPivot;
    public float openAngle = 90f; 
    public float smoothSpeed = 5f;

    [Header("Cooking")]
    public GameObject cookedKeyPrefab;
    public Transform spawnPoint;

    private bool isOpen = false;
    private bool keyInside = false;
    private GameObject currentKey;
    private bool keyCooked;
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
        mousingM = false;
        CheckInteraction();
        Quaternion targetRotation = isOpen ? openRotation : closedRotation;
        doorPivot.localRotation = Quaternion.Slerp(doorPivot.localRotation, targetRotation, Time.deltaTime * smoothSpeed);

        if (Input.GetMouseButtonDown(0) && mousingM) 
        {
            ToggleDoor();
            return;
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
        if(keyCooked && !isOpen)
        {
            isOpen = true;
            return;
        }
        isOpen = !isOpen;
        if (!isOpen && keyInside)
        {
            CookKey();
        }
        //var fText = GetComponentInChildren<TMP_Text>();
        //if (fText != null)
        //{
        //    fText.text = isOpen ? "Microwave\nOpen" : "Microwave\nClosed";
        //}
        Debug.Log(isOpen ? "Microwave opened" : "Microwave closed");
    }
    void CookKey()
    {
        Debug.Log("Cooking the key...");
        if (currentKey != null)
        {
            Destroy(currentKey);
        }
        if (cookedKeyPrefab != null)
        {
            var obj = Instantiate(cookedKeyPrefab, spawnPoint.position, spawnPoint.rotation);
            obj.GetComponent<PlacementEmitter>().previewMeshes[0] = GameObject.Find("cookedKeyPreviewMeshSolid").GetComponent<MeshRenderer>();
            obj.GetComponent<PlacementEmitter>().previewHighlight = GameObject.Find("cookedKeyPreviewHighlight").GetComponent<MeshRenderer>();
            keyCooked = true;
            GetComponent<BoxCollider>().enabled = false;
        }
        keyInside = false;
        currentKey = null;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MouldedKey") && isOpen)
        {
            keyInside = true;
            currentKey = other.gameObject;
            Debug.Log("Moulded key placed inside microwave.");
        }
    }
    private float PlayerDistance()
    {
        float distance = Vector3.Distance(Player.transform.position, transform.position);
        return distance;
    }
}