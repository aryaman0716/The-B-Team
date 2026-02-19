using UnityEngine;

public class TomatoProjectile : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Destroy the tomato after hitting something
        Destroy(gameObject);
    }
}