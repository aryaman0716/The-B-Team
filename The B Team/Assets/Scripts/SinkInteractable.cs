using UnityEngine;
public class SinkInteractable : MonoBehaviour
{
    public ParticleSystem water;
    public AudioSource faucetSound;
    private bool isOn = false;
    private EquipmentController equipment;
    public GameObject Player;
    public static bool mousingS = false;
    public static bool anyfocus = false;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        equipment = FindFirstObjectByType<EquipmentController>();
    }
    void OnMouseOver()
    {
        if (EquipmentController.publicIndex < 4)
        {
            return;
        }
        if (Player != null)
        {
            if (PlayerDistance() > 3f) return;
        }
        mousingS = true;
        if (Input.GetMouseButtonDown(0))
        {
            ToggleSink();
        }
    }
    void ToggleSink()
    {
        isOn = !isOn;
        if (isOn)
        {
            if (water != null)
                water.Play();
            if (faucetSound != null)
                faucetSound.Play();
        }
        else
        {
            if (water != null)
                water.Stop();
            if (faucetSound != null)
                faucetSound.Stop();
        }
    }
    public bool IsOn()
    {
        return isOn;
    }
    private float PlayerDistance()
    {
        float distance = Vector3.Distance(Player.transform.position, transform.position);
        return distance;
    }
}
