using UnityEngine;
public class SinkInteractable : MonoBehaviour
{
    public ParticleSystem water;
    public AudioSource faucetSound;
    private bool isOn = false;
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ToggleSink();
        }
    }
    void ToggleSink()
    {
        isOn = !isOn;
        if (isOn)
        {
            if (water != null)
                water.Play();
            if (faucetSound != null)
                faucetSound.Play();
        }
        else
        {
            if (water != null)
                water.Stop();
            if (faucetSound != null)
                faucetSound.Stop();
        }
    }
}
