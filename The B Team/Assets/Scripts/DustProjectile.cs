using UnityEngine;
public class DustProjectile : MonoBehaviour
{
    public float speed = 5f;
    void Start()
    {
        Destroy(gameObject, 4f);
    }
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    void OnTriggerEnter(Collider other)
    {
        VentFlourTarget vent = other.GetComponent<VentFlourTarget>();
        if (vent != null)
        {
            vent.RevealLasers();
            Destroy(gameObject);
        }

        KeypadFlourTarget keypad = other.GetComponent<KeypadFlourTarget>();
        if (keypad != null)
        {
            keypad.RevealButtons();
            Destroy(gameObject);
        }
    }
}
