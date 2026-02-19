using UnityEngine;
using System.Collections;
public class ScrewInteractable : MonoBehaviour
{
    public VentSystem ventSystem;
    public float unscrewTime = 5f;
    public AudioSource unscrewSound;
    public EquipmentController equipment;
    public int knifeIndex = 0;

    private bool isRemoved = false;
    private bool isUnscrewing = false;  
    void OnMouseOver()
    {
        if (isRemoved) return;
        if (!ventSystem.isActivated) return;
        if (equipment.GetCurrentIndex() != knifeIndex) return;

        if (Input.GetMouseButtonDown(1) && !isUnscrewing)
        {
            StartCoroutine(Unscrew());
        }
    }
    IEnumerator Unscrew()
    {
        isUnscrewing = true;
        float timer = 0f;

        while (timer < unscrewTime)
        {
            // Stop unscrewing if the player releases the button
            if (!Input.GetMouseButton(1))
            {
                isUnscrewing = false;
                yield break;  
            }
            timer += Time.deltaTime;
            yield return null;
        }
        isRemoved = true;

        if (unscrewSound != null)
        {
            unscrewSound.Play();
        }
        ventSystem.ScrewRemoved();
        yield return new WaitForSeconds(0.1f); // Wait for the sound to play before destroying
        Destroy(gameObject);
    }
}
