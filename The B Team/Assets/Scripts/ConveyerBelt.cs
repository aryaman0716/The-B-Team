using UnityEngine;

public class ConveyerBelt : MonoBehaviour
{
    public float speed = 3f;
    public bool on = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void turnOff()
    {
        Debug.Log("off");
        foreach (Transform child in transform)
        {
            child.gameObject.tag = "Untagged";
        }
        GetComponent<AudioSource>().Stop();
        GetComponent<ScrollMaterial>().scroll = false;
    }


}
