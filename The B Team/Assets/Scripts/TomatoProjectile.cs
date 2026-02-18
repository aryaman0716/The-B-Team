using UnityEngine;

public class TomatoProjectile : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        ButtonTarget button = collision.collider.GetComponent<ButtonTarget>();

        if (button != null)
        {
            button.Activate();
        }
        //Destroy(gameObject);
    }
}
