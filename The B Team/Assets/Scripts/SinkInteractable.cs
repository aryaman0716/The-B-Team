using UnityEngine;
public class SinkInteractable : MonoBehaviour
{
    public ParticleSystem water;
    public AudioSource faucetSound;
    private bool isOn = false;
    public bool IsOn => isOn;
    private bool filled = false;
    public bool Filled => filled;
    private EquipmentController equipment;
    public GameObject Player;
    public static bool mousingS = false;
    public static bool anyfocus = false;
    private GameObject water_obj;
    public Outline outline;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        equipment = FindFirstObjectByType<EquipmentController>();
        water_obj = GameObject.Find("water_obj");
        water_obj.SetActive(false);
        filled = false;
    }
    void OnMouseOver()
    {
        if (UIController.Paused) { outline.enabled = false; mousingS = false; }
        if (Player != null)
        {
            if (PlayerDistance() > 3f) return;
        }
        mousingS = true;
        outline.enabled = true;
        if (Input.GetMouseButtonDown(0) && !filled)
        {
            ToggleSink(!isOn);
        }
    }
    void OnMouseExit()
    {
        mousingS = false;
        outline.enabled = false;
    }
    void Update()
    {
        if (filled) { return; }
        if(!water_obj.activeSelf) { return; }
        if (water_obj.GetComponent<SinkWater>().sinkFilled)
        {
            filled = true;
            mousingS = false;
            ToggleSink(false);
        }
    }
    public void ToggleSink(bool val)
    {
        isOn = val;
        if (val == true)
        {
            if (water != null)
                water.Play();
            if (faucetSound != null)
                faucetSound.Play();
            if (water_obj != null)
                water_obj.SetActive(true);
                water_obj.GetComponent<Animator>().speed = 1;
        }
        else
        {
            if (water != null)
                water.Stop();
            if (faucetSound != null)
                faucetSound.Stop();
            if (water_obj != null)
                water_obj.GetComponent<Animator>().speed = 0;
        }
    }

    private float PlayerDistance()
    {
        float distance = Vector3.Distance(Player.transform.position, transform.position);
        return distance;
    }

    public void ButtonSink()
    {
        ToggleSink(!isOn);
    }
    
}
