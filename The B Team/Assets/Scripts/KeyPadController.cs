using UnityEngine;

public class KeyPadController : MonoBehaviour
{
    public string keyCode;
    public string currentCode;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCode == keyCode)
        {
            CorrectCodeFunction();
        }

        

    }

    public void AddNumber(string number)
    {
        currentCode = currentCode + number;

        if (currentCode.Length > 4)
        {
            currentCode = currentCode.Substring(currentCode.Length - 4);
        }
        Debug.Log(currentCode);
    }

    public void CorrectCodeFunction()
    {
        Debug.Log("Open the door or whatever, correct code entered");
    }
}
