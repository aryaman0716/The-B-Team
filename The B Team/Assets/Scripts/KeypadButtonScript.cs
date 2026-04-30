using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
public class KeypadButtonScript : MonoBehaviour
{
    public string buttonID;
    public KeyPadController controller;
    private GameObject Player;

    public bool keyPad = true;
    [SerializeField] private UnityEvent buttonFunction;
    public bool tomatoButton = false;

    public AudioSource buttonSource;
    public AudioClip buttonSound;
    public bool needsKey = false;
    public static bool mousingB = false;
    private Outline outline;

    private KeypadButtonScript currentButton;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        outline = GetComponent<Outline>();
    }
    void Update()
    {
        if (outline != null && mousingB) 
        { 
            if(currentButton == this)
            {
                outline.enabled = true;
            }
            else
            {
                outline.enabled = false;
            }
            
        }
    }
    public void PressButton()
    {
        if (needsKey) return;
        if (keyPad)
        {
            if (controller != null)
                controller.AddNumber(buttonID);
        }
        else
        {
            buttonFunction.Invoke();
        }
        if (buttonSound != null && buttonSource != null)
        {
            buttonSource.volume = 0.25f * GlobalSettings.MasterVolume * GlobalSettings.SFXVolume;
            buttonSource.clip = buttonSound;
            buttonSource.Play();
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
                mousingB = true;
                currentButton = this;
            }
        }

    }
    void OnMouseDown()
    {
        if (EquipmentController.publicIndex < 4)
        {
            return;
        }
        if (controller != null && controller.focusController != null && controller.focusController.IsFocused)
        {
            PressButton();
            return;
        }

        if (Player != null)
        {
            if (PlayerDistance() < 3f) PressButton();
        }
        else
        {
            PressButton();
        }
    }
    void OnMouseExit()
    {
        mousingB = false;
        if (outline != null) { outline.enabled = false; }
        if (currentButton == this) { currentButton = null; }
    }

    private float PlayerDistance()
    {
        float distance = Vector3.Distance(Player.transform.position, transform.position);
        return distance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("TomatoProjectile") && tomatoButton)
        {
            PressButton();
        }
    }
}