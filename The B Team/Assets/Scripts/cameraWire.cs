using UnityEngine;

public class cameraWire : MonoBehaviour
{
    public GameObject cameraView;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Cut()
    {
        cameraView.SetActive(false);
        gameObject.SetActive(false);

        ObjectiveManager.Instance.CompleteObjective("Find a way to turn off the security camera.");
        ObjectiveManager.Instance.SetObjective("Find a keycard to unlock the staff elevator.");
    }
}
