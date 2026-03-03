using UnityEngine;
public class SinkInteractable : MonoBehaviour
{
    public ParticleSystem water;
    public AudioSource faucetSound;
    private bool isOn = false;
    private EquipmentController equipment;
    void Start()
    {
        equipment = FindFirstObjectByType<EquipmentController>();
    }
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (equipment != null && equipment.GetCurrentIndex() != equipment.TotalTools())
            {
                Debug.Log("Can't use sink while holding a tool.");
                return;
            }
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
}
