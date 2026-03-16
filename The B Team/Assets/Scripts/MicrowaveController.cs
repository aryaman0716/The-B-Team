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
    private bool keyCooked = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private EquipmentController equipment;

    void Start()
    {
        closedRotation = doorPivot.localRotation;
        openRotation = Quaternion.Euler(0, openAngle, 0);
        equipment = FindFirstObjectByType<EquipmentController>();
    }
    void Update()
    {
        Quaternion targetRotation = isOpen ? openRotation : closedRotation;
        doorPivot.localRotation = Quaternion.Slerp(doorPivot.localRotation, targetRotation, Time.deltaTime * smoothSpeed);
        
        if (Input.GetMouseButtonDown(0)) 
        {
            CheckInteraction();
        }
    }
    void CheckInteraction()
    {
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
        ToggleDoor();
        TMP_Text text = GetComponentInChildren<TMP_Text>();
        if (text == null) return;
        if (isOpen)
        {
            text.text = "Microwave\nOpen";
        }
        else
        {
            text.text = "Microwave\nClosed";
        }
    }
    public void ToggleDoor()
    {
        if (keyCooked)
        {
            isOpen = true;
            GetComponent<MicrowaveController>().enabled = false;
        }
        isOpen = !isOpen;
        if (!isOpen && keyInside)
        {
            CookKey();
        }
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
            Instantiate(cookedKeyPrefab, spawnPoint.position, spawnPoint.rotation);
            keyCooked = true;
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
}