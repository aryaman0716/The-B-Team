using UnityEngine;
using UnityEngine.Events;

public class KeypadButtonScript : MonoBehaviour
{
    public string buttonID;
    public KeyPadController controller;
    private GameObject Player;

    //if you want the button to be for a keypad or a general use button
    public bool keyPad = true;
    [SerializeField] private UnityEvent buttonFunction;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
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

        if (Player!=null)
        {
            if (PlayerDistance() < 3f) PressButton();
        }
        else PressButton();
    }


    private float PlayerDistance()
    {
        float distance = Vector3.Distance(Player.transform.position, transform.position);
        return distance;
    }
}