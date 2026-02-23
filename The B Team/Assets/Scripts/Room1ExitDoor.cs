using UnityEngine;
public class Room1ExitDoor : MonoBehaviour
{
    public Animator animator;
    private bool opened = false;
    public void ApplyOil()
    {
        if (opened) return;
        opened = true;
        if (animator != null)
        {
            animator.SetTrigger("Open");
        }
    }
}
