using UnityEngine;
using System.Collections;
public class KeypadFocusController : MonoBehaviour
{
    public Transform focusPoint;
    public float focusFOV = 30f;
    public float normalFOV = 60f;

    private Camera playerCamera;
    public FPController controller;
    public GameObject propsHolder;

    private Vector3 originalPos;
    private Quaternion originalRot;
    private bool isFocused = false;

    void Start()
    {
        playerCamera = Camera.main;
    }
    public bool IsFocused => isFocused;
    public void EnterFocusMode()
    {
        if (isFocused) return;
        isFocused = true;
        Headbob.canBob = false;

        originalPos = playerCamera.transform.position;
        originalRot = playerCamera.transform.rotation;

        if (controller != null)
        {
            controller.SetCanMove(false);
            controller.enabled = false;
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (propsHolder != null) propsHolder.SetActive(false);
        StartCoroutine(SmoothFocus(focusPoint.position, focusPoint.rotation, focusFOV));
    }

    public void ExitFocusMode()
    {
        if (!isFocused) return;
        isFocused = false;

        if (controller != null)
        {
            controller.enabled = true;
            controller.SetCanMove(true);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (propsHolder != null) propsHolder.SetActive(true);
        StartCoroutine(SmoothFocus(originalPos, originalRot, normalFOV));
        Headbob.canBob = true;
    }

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