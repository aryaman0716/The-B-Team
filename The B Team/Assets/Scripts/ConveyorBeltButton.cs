using UnityEngine;
using UnityEngine.Events;

public class ConveyorBeltButton : MonoBehaviour
{
    public string id;
    public UnityEvent activated;
    public UnityEvent deactivated;
    public AudioClip buttonSound;
    public AudioSource buttonSource;
    private GameObject Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buttonSource = GetComponent<AudioSource>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ActivateButton()
    {
        ConveyorBeltController.ButtonActivated(this);
        if (buttonSound != null && buttonSource != null)
        {
            buttonSource.volume = 0.25f * GlobalSettings.MasterVolume * GlobalSettings.SFXVolume;
            buttonSource.clip = buttonSound;
            buttonSource.Play();
        }
    }

    void DeactivateButton()
    {
        deactivated.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("TomatoProjectile"))
        {
            Debug.Log("tomato hit");
            ActivateButton();
        }
    }
    public void OnMouseOver()
    {
        Debug.Log("mouse over button" + PlayerDistance());
        if (EquipmentController.publicIndex < 4)
        {
            return;
        }
        if (Player != null)
        {
            if (PlayerDistance() < 3f)
            {
                KeypadButtonScript.mousingB = true;
            }
        }

    }
    void OnMouseDown()
    {
        if (EquipmentController.publicIndex < 4)
        {
            return;
        }

        if (Player != null)
        {
            if (PlayerDistance() < 3f) ActivateButton();
        }
    }
    private float PlayerDistance()
    {
        float distance = Vector3.Distance(Player.transform.position, transform.position);
        return distance;
    }

}
