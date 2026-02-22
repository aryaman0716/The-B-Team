using UnityEngine;

public class KeyPadController : MonoBehaviour
{
    public string keyCode = "1234";
    public string currentCode;
    public KeypadFocusController focusController;

    [Header("Flour System")]
    public MeshRenderer[] targetButtons; 
    public Material highlightedMaterial; 

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
        if (currentCode.Length > 4)
            currentCode = currentCode.Substring(currentCode.Length - 4);
        Debug.Log("Current Input: " + currentCode);
    }

    public void CorrectCodeFunction()
    {
        Debug.Log("Unlocked!");
        if (focusController != null) focusController.ExitFocusMode();
    }


    void OnMouseOver()
    {
        
        if (Input.GetMouseButtonDown(1))
        {
            if (focusController != null) focusController.EnterFocusMode();
        }
    }
}