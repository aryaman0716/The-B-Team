using UnityEngine;

public class KeypadButtonScript : MonoBehaviour
{
    public string buttonID;
    public KeyPadController controller;

    public void PressButton()
    {
        if (controller != null)
            controller.AddNumber(buttonID);
    }

   
    void OnMouseDown()
    {
        PressButton();
    }
}