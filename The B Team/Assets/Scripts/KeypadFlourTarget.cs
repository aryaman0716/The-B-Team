using UnityEngine;
public class KeypadFlourTarget : MonoBehaviour
{
    public KeyPadController keyPad;
    public void RevealButtons()
    {
        if (keyPad != null)
        {
            keyPad.ApplyFlour();
        }
    }
}
