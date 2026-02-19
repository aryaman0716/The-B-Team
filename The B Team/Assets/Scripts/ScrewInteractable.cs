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

        
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Right Click Detected on Screw!");

            if (!ventSystem.isActivated)
            {
                Debug.Log("Cannot unscrew: Vent is not activated (hit button with tomato first)");
                return;
            }

            int currentEquipped = equipment.GetCurrentIndex();
            if (currentEquipped != knifeIndex)
            {
                Debug.Log("Cannot unscrew: Wrong tool! Holding index: " + currentEquipped + " but need: " + knifeIndex);
                return;
            }

            if (!isUnscrewing)
            {
                Debug.Log("Starting Unscrew Coroutine...");
                StartCoroutine(Unscrew());
            }
        }
    }
    IEnumerator Unscrew()
    {
        isUnscrewing = true;
        float timer = 0f;
        Debug.Log("Unscrewing... hold right click for 5 seconds");

        while (timer < unscrewTime)
        {
            if (!Input.GetMouseButton(1))
            {
                Debug.Log("Unscrew interrupted!");
                isUnscrewing = false;
                yield break;
            }
            timer += Time.deltaTime;
            yield return null;
        }

        isRemoved = true;
        isUnscrewing = false;

       
        if (unscrewSound != null)
        {
            unscrewSound.Play();
            Debug.Log("Screw dropped! Sound playing.");
        }

        ventSystem.ScrewRemoved();

        
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
}
