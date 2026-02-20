using UnityEngine;

public class TomatoProjectile : MonoBehaviour
{
    public GameObject Splash;
    private void OnCollisionEnter(Collision collision)
    {
        GameObject instance = Instantiate(Splash, transform.position, Quaternion.identity);
        instance.transform.SetParent(null);
        // Destroy the tomato after hitting something
        Destroy(gameObject);
    }
    private void Start()
    {
        //give tomatoes a random spin when thrown
        Vector3 randomAngularVelocity = Random.insideUnitSphere * Random.Range(-5, 20);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.angularVelocity = randomAngularVelocity;

    }
}