using UnityEngine;

public class keyCardReceptical : MonoBehaviour
{
    public KeypadButtonScript button, button2;
    public PlacementEmitter keyCardEmitter;

    public MeshRenderer screenMesh;
    public Material[] materials;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        screenMesh.material = materials[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (keyCardEmitter.gameObject.activeSelf && keyCardEmitter.IsPlaced)
        {
            screenMesh.material = materials[1];
            button.needsKey = false;
            button2.needsKey = false;
            keyCardEmitter.gameObject.SetActive(false);
            ObjectiveManager.Instance.CompleteObjective("Find a keycard to unlock the staff elevator.", "Find a way to blast through the door");
        }
        
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("KeyCard"))
    //    {
    //        button.needsKey = false;
    //        button2.needsKey = false;
    //        collision.gameObject.SetActive(false);
    //    }
    //    ObjectiveManager.Instance.CompleteObjective("Find a keycard to unlock the staff elevator.", "Find a way to blast through the vault door.");
    //}
}
