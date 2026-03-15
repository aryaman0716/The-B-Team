using UnityEngine;
using UnityEngine.Events;

public class KeyPadController : MonoBehaviour
{
    public string keyCode = "1234";
    public string currentCode;
    public KeypadFocusController focusController;

    [Header("Flour System")]
    public MeshRenderer[] targetButtons; 
    public Material highlightedMaterial;

    //use this in the editor to assign a public funtion of another script to the keypad when correct code is done
    [SerializeField] private UnityEvent keyFunction;

    public AudioSource keypadSource;
    public AudioClip correctClip;
    void Update()
    {
        if (currentCode == keyCode)
        {
            CorrectCodeFunction();
            currentCode = ""; // Reset
        }
    }

    
    public void ApplyFlour()
    {
        foreach (MeshRenderer btn in targetButtons)
        {
            if (btn != null) btn.material = highlightedMaterial;
        }
        Debug.Log("Keypad: Flour applied to buttons 1, 2, 3, 4");
    }

    public void AddNumber(string number)
    {
        currentCode = currentCode + number;
        if (currentCode.Length > keyCode.Length)
            currentCode = currentCode.Substring(currentCode.Length - keyCode.Length);
        Debug.Log("Current Input: " + currentCode);
    }

    public void CorrectCodeFunction()
    {
        Debug.Log("Unlocked!");
        if (focusController != null) focusController.ExitFocusMode();
        if (keyFunction != null) keyFunction.Invoke();
        if (keypadSource != null && correctClip != null)
        {
            keypadSource.clip = correctClip;
            keypadSource.volume = 0.25f * GlobalSettings.MasterVolume * GlobalSettings.SFXVolume;
            keypadSource.Play();
        }
    }


    void OnMouseOver()
    {
        
        if (Input.GetMouseButtonDown(1))
        {
            if (focusController != null) focusController.EnterFocusMode();
        }
    }
}