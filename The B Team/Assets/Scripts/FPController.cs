using UnityEngine;

public class FPController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float gravity = -20f;
    [SerializeField] private float jumpHeight = 1.5f;

    [Header("Mouse Settings")]
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float lookXLimit = 80f;

    [Header("References")]
    [SerializeField] private Transform cameraHolder;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0f;
    private bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        if (canMove)
        {
            HandleMovement();
            HandleMouseLook();
        }
        // Unlock cursor on Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        // Lock cursor on left mouse button click
        if (Input.GetMouseButtonDown(0) && Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calculate the direction in which the player should move
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        // Calculate movement direction while preserving vertical velocity
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * moveZ) + (right * moveX);
        moveDirection = moveDirection * currentSpeed;

        if (Input.GetButton("Jump") && characterController.isGrounded)
        {
            moveDirection.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }
        // Apply gravity
        if (!characterController.isGrounded)
        {
            moveDirection.y += gravity * Time.deltaTime;
        }
        characterController.Move(moveDirection * Time.deltaTime);
    }
    void HandleMouseLook()
    {
        // Rotate the camera vertically
        rotationX -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        cameraHolder.localRotation = Quaternion.Euler(rotationX, 0, 0);

        // Rotate the player horizontally
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * mouseSensitivity, 0);
    }
    public void SetCanMove(bool value)
    {
        canMove = value;
    }
}
