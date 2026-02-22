using UnityEngine;

public class FPController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float gravity = -20f;
    [SerializeField] private float jumpHeight = 1.5f;

    [Header("Crouch Settings")]
    [SerializeField] private float crouchHeight = 1f;
    [SerializeField] private float standingHeight = 2f;
    [SerializeField] private float crouchSpeed = 2.5f;
    [SerializeField] private float smoothCrouchSpeed = 5f;
    [SerializeField] private float standingCameraHeight = 1.6f;
    [SerializeField] private float crouchingCameraHeight = 0.8f;

    [Header("Mouse Settings")]
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float lookXLimit = 80f;

    [Header("References")]
    [SerializeField] private Transform cameraHolder;

    [Header("Level Loading")]
    [SerializeField] private LevelLoader levelLoader;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0f;
    private bool canMove = true;
    private bool paused;
    private bool isCrouching = false;

    public float ButtonPressDistance = 4f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        paused = UIController.Paused;

        if (canMove && !paused)
        {
            HandleMovement();
            HandleCrouch();
            HandleMouseLook();
            //HandleButtonRaycast();
        }

        if (Input.GetKeyDown(KeyCode.L)) // press L change Scene
        {
            levelLoader.LoadLevel(0); // Index Scene 
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
        float currentSpeed;

        if (isCrouching)
        {
            currentSpeed = crouchSpeed;
        }
        else
        {
            currentSpeed = isRunning ? runSpeed : walkSpeed;
        }

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
    void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = !isCrouching;
        }

        float targetHeight = isCrouching ? crouchHeight : standingHeight;
        characterController.height = Mathf.Lerp(characterController.height, targetHeight, Time.deltaTime * smoothCrouchSpeed);
        characterController.center = new Vector3(0, characterController.height / 2f, 0);

        float targetCameraHeight = isCrouching ? crouchingCameraHeight : standingCameraHeight;
        Vector3 camPos = cameraHolder.localPosition;
        camPos.y = Mathf.Lerp(camPos.y, targetCameraHeight, Time.deltaTime * smoothCrouchSpeed);
        cameraHolder.localPosition = camPos;
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

    public void HandleButtonRaycast()
    {
        //basically what this does is do a raycast forward from the camera when you press left mouse and if it hits a button it runs the press function on that button, might be better with the interact system but i didnt make that so

        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, ButtonPressDistance))
            {
                KeypadButtonScript button = hit.collider.GetComponent<KeypadButtonScript>();

                if (button != null)
                {
                    button.PressButton();

                }

            }
        }
    }
    public void ResetPlayerState()
    {
        // Ensure we have a valid CharacterController reference
        if (characterController == null)
            characterController = GetComponent<CharacterController>();

        isCrouching = false;
        moveDirection = Vector3.zero;

        bool wasEnabled = characterController.enabled;
        characterController.enabled = false;

        characterController.height = standingHeight;
        characterController.center = new Vector3(0, standingHeight / 2f, 0);

        characterController.enabled = wasEnabled;

        Vector3 camPos = cameraHolder.localPosition;
        camPos.y = standingCameraHeight;
        cameraHolder.localPosition = camPos;

        characterController.Move(Vector3.zero);
    }
}
