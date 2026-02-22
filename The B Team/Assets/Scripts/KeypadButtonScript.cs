using UnityEngine;
using UnityEngine.Events;

public class KeypadButtonScript : MonoBehaviour
{
    public string buttonID;
    public KeyPadController controller;


    //if you want the button to be for a keypad or a general use button
    public bool keyPad = true;
    [SerializeField] private UnityEvent buttonFunction;

    public void PressButton()
    {
        if (keyPad)
        {
            if (controller != null)
            controller.AddNumber(buttonID);
        }
        else
        {
            buttonFunction.Invoke();
        }
    }

   
    void OnMouseDown()
    {
        PressButton();
    }
}