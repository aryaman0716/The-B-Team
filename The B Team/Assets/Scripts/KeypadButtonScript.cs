using UnityEngine;

public class KeypadButtonScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public string buttonID;
    public KeyPadController controller;
    void Start()
    {

    }

    public void PressButton()
    {
        controller.AddNumber(buttonID);
    }
}
