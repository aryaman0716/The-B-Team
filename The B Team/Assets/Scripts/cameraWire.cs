using UnityEngine;

public class cameraWire : MonoBehaviour
{
    public GameObject cameraView;
    public static bool mousing = false;

    public EquipmentController equipment;
    private int knifeIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        equipment = FindFirstObjectByType<EquipmentController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver()
    {
        if (!gameObject.activeSelf) { return; }
        Debug.Log(EquipmentController.DistanceToPlayer(transform));
        if (EquipmentController.DistanceToPlayer(transform) > 4.5f)
        {
            GetComponent<Outline>().enabled = false;
            mousing = false;
            return;
        }
        GetComponent<Outline>().enabled = true;
        mousing = true;
    }

    void OnMouseExit()
    {
        if (!gameObject.activeSelf) { return; }
        GetComponent<Outline>().enabled = false; 
        mousing = false;
        
    }

    public void Cut()
    {
        mousing = false;
        GetComponent<Outline>().enabled = false;
        cameraView.SetActive(false);
        gameObject.SetActive(false);

        ObjectiveManager.Instance.CompleteObjective("Find a way to turn off the security camera.", "Find a keycard to unlock the staff elevator.");
    }
}
