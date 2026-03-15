using UnityEngine;

public class MicrowaveDoor : MonoBehaviour
{
    [SerializeField] private Animator a;
    private bool open;

    [SerializeField] private EquipmentController equipment;

    void Update()
    {

    }

    void OnMouseOver()
    {
        if (!a.enabled) { Debug.Log("Animator disabled"); return; }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click");
            if (equipment != null && equipment.GetCurrentIndex() != equipment.TotalTools())
            {
                Debug.Log("Can't use microwave while holding a tool.");
                return;
            }

            if (a.GetBool("DoorState") == false)
            {
                SetDoor(true);
                return;
            }
            else
            {
                SetDoor(false);
                return;
            }
        }
    }
    
    void SetDoor(bool val)
    {
        a.SetBool("DoorState",val);
    }
}
