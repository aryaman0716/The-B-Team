using UnityEngine;
using UnityEngine.Events;

public class ConveyorBeltButton : MonoBehaviour
{
    public string id;
    public UnityEvent activated;
    public UnityEvent deactivated;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ActivateButton()
    {
        ConveyorBeltController.ButtonActivated(this);
    }

    void DeactivateButton()
    {
        deactivated.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("TomatoProjectile"))
        {
            Debug.Log("tomato hit");
            ActivateButton();
        }
    }
}
