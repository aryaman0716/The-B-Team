using UnityEngine;

public class Ladel : MonoBehaviour
{
    public GameObject sauce;
    public bool saucy = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sauce.SetActive(saucy); 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sauce"))
        {
            saucy = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Ice ice = collision.gameObject.GetComponent<Ice>();
        if (ice != null && saucy)
        {
            ice.Melt();
            saucy=false;
        }
    }
}
