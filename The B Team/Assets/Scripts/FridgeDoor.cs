using UnityEngine;

public class FridgeDoor : MonoBehaviour
{
    public Animator animator;
    private bool opened = false;

    public static bool mousing = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Outline>().enabled = mousing;
    }
    public void Pry()
    {
        if (opened) return;
        opened = true;
        if (animator != null)
        {
            animator.SetTrigger("Open");
        }
    }

    void OnMouseOver()
    {
        if (opened) { mousing = false; return; }
        if (EquipmentController.DistanceToPlayer(transform) > 3f) { mousing = false; return; }
        mousing = true;
    }
    void OnMouseExit()
    {
        mousing = false;
    }
}
