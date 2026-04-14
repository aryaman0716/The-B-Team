using UnityEngine;

public class keyCardReceptical : MonoBehaviour
{
    public KeypadButtonScript button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("KeyCard"))
        {
            button.needsKey = false;
            collision.gameObject.SetActive(false);
        }
        ObjectiveManager.Instance.CompleteObjective("Find a keycard to unlock the staff elevator.");
        ObjectiveManager.Instance.SetObjective("Find a way to blast through the vault door.");
    }
}
