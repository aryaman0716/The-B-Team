using UnityEngine;

public class CameraPan : MonoBehaviour
{
    public float maxHorizontal = 5f;     
    public float maxVertical = 3f;

    public float smoothSpeed = 5f;

    private Quaternion startRotation;

    void Start()
    {
        startRotation = transform.rotation;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        float mouseX = (Input.mousePosition.x / Screen.width) * 2f - 1f;
        float mouseY = (Input.mousePosition.y / Screen.height) * 2f - 1f;

        mouseX = Mathf.Clamp(mouseX, -1f, 1f);
        mouseY = Mathf.Clamp(mouseY, -1f, 1f);

        float yaw = mouseX * maxHorizontal;
        float pitch = -mouseY * maxVertical;

        Quaternion targetRotation = startRotation * Quaternion.Euler(pitch, yaw, 0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * smoothSpeed);
        
    }
}