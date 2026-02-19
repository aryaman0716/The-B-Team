using UnityEngine;
using System.Collections;
public class VentFocusController : MonoBehaviour
{
    public Transform focusPoint;
    public float focusFOV = 30f;  // Field of view when vent is in focus
    public float normalFOV = 60f; // Normal field of view

    private Camera playerCamera;
    private FPController controller;
    private Vector3 originalPos;
    private Quaternion originalRot;
    private bool isFocused = false;
    void Start()
    {
        playerCamera = Camera.main;
        controller = playerCamera.GetComponent<FPController>();
    }
    public void EnterFocusMode()
    {
        if (isFocused) return;
        isFocused = true;
        originalPos = playerCamera.transform.position;
        originalRot = playerCamera.transform.rotation;

        controller.SetCanMove(false);
        StartCoroutine(SmoothFocus(focusPoint.position, focusPoint.rotation, focusFOV));
    }
    public void ExitFocusMode()
    {
        if (!isFocused) return;
        isFocused = false;
        controller.SetCanMove(true);
        StartCoroutine(SmoothFocus(originalPos, originalRot, normalFOV));
    }

    // Smoothly transition the camera to the target position, rotation, and FOV
    IEnumerator SmoothFocus(Vector3 targetPos, Quaternion targetRot, float targetFOV)
    {
        float t = 0f;
        Vector3 startPos = playerCamera.transform.position;
        Quaternion startRot = playerCamera.transform.rotation;
        float startFOV = playerCamera.fieldOfView;

        while (t < 1f)
        {
            t += Time.deltaTime * 2f;
            playerCamera.transform.position = Vector3.Lerp(startPos, targetPos, t);
            playerCamera.transform.rotation = Quaternion.Slerp(startRot, targetRot, t);
            playerCamera.fieldOfView = Mathf.Lerp(startFOV, targetFOV, t);

            yield return null;
        }
    }
}
