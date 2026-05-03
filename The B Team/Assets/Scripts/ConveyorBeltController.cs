using UnityEngine;
using UnityEngine.Events;
public class ConveyorBeltController : MonoBehaviour
{
    public ConveyorBeltButton[] buttons;
    public string correctCode;
    public string currentCode;

    public bool correctCodeEntered;

    public string nextCorrectValue;
    public int codeStep;
    [SerializeField] private UnityEvent correctEvent;
    private static ConveyorBeltController instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource keypadSource;
    public AudioClip correctClip;
    void Start()
    {
        instance = this;
        nextCorrectValue = correctCode[0].ToString();
        codeStep = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!enabled) { return; }
        //if (Input.GetMouseButtonDown(0) && !correctCodeEntered)
        //{
        //    SetNextCorrectValue();
        //}
    }

    public void SetNextCorrectValue()
    {
       
        if((codeStep+1) < correctCode.Length)
        {
            currentCode += correctCode[codeStep].ToString();
            codeStep++;
            nextCorrectValue = correctCode[codeStep].ToString();
        }
        else
        {
            currentCode += correctCode[codeStep].ToString();
            correctCodeEntered = true;
            correctEvent.Invoke();
            if (keypadSource != null && correctClip != null)
            {
                keypadSource.clip = correctClip;
                keypadSource.volume = 0.25f * GlobalSettings.MasterVolume * GlobalSettings.SFXVolume;
                keypadSource.Play();
            }
            enabled = false;
        }
    }

    public static void ButtonActivated(ConveyorBeltButton button)
    {
        if(button.id == instance.nextCorrectValue)
        {
            instance.SetNextCorrectValue();
            button.activated.Invoke();
        }
        else
        {
            instance.ResetCodeProgress();
        }
    }

    private void ResetCodeProgress()
    {
        foreach (ConveyorBeltButton b in buttons)
        {
            b.deactivated.Invoke();
        }
        codeStep = 0;
        currentCode = string.Empty;
        nextCorrectValue = correctCode[0].ToString();
    }


}
