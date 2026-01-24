using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("Hello Unity");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Start Unity");

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 2,0);
        Debug.Log("End Unity");
    }
}
