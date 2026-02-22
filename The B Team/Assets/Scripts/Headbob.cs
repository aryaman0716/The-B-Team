using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class Headbob : MonoBehaviour
{
    public bool Enabled = true;
    [SerializeField, Range(0, 0.1f)] private float Amplitude = 0.015f;
    [SerializeField, Range(0, 30.0f)] private float Frequency = 10f;
    public Transform Camera;
    public Transform CameraHolder;
    private float ToggleSpeed = 3f;
    private Vector3 StartPos;
    private CharacterController Controller;

    private Vector3 toolStartPos;

    public GameObject ToolHolder;
    public static bool canBob = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Controller = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        StartPos = Camera.localPosition;
        toolStartPos = ToolHolder.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Enabled = GlobalSettings.HeadBob;

        if (Time.timeScale < 0.1f) return;
        if (!Enabled) return;
        if (!canBob) return;

        CheckMotion();
        Camera.LookAt(FocusTarget());
    }
    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * Frequency) * Amplitude;
        pos.x += Mathf.Cos(Time.time * Frequency/2) * Amplitude / 2;
        return pos;
    }
    private void CheckMotion()
    {
        float speed = new Vector3(Controller.velocity.x, 0, Controller.velocity.z).magnitude;
        ResetPosition();
        if (speed < ToggleSpeed) return;
        if (!Controller.isGrounded) return;

        PlayMotion(FootStepMotion());

    }
    private void PlayMotion(Vector3 motion)
    {
        Camera.localPosition += motion;
        if (ToolHolder != null)
        {
            ToolHolder.transform.localPosition += motion / 8;
        }

    }
    private void ResetPosition()
    {
        if (Camera.localPosition == StartPos) return;
        Camera.localPosition = Vector3.Lerp(Camera.localPosition, StartPos, Time.deltaTime * 2);

        if (ToolHolder != null)
        {
            if (ToolHolder.transform.localPosition == toolStartPos) return;
            ToolHolder.transform.localPosition = Vector3.Lerp(ToolHolder.transform.localPosition, toolStartPos, Time.deltaTime * 2);
        }
    }
    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + CameraHolder.localPosition.y, transform.position.z);
        pos += CameraHolder.forward * 15.0f;
        return pos;
    }
}
