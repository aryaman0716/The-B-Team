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
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
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