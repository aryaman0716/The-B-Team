using UnityEngine;

public class ConveyerBelt : MonoBehaviour
{
    public float speed = 3f;
    public bool on = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void turnOff()
    {
        on = false;
    }
    public void HandleCollision(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && on)
        {
            Debug.Log("onbelt");
            collision.transform.position += new Vector3(0, 0, speed * Time.deltaTime);
        }
    }

}
