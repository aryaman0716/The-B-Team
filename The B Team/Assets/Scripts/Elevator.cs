using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Elevator : MonoBehaviour
{
    [Header("Elevator Positions (World Y)")]
    public float startYPos;
    public float endYPos;

    [Header("Movement Settings")]
    public float moveSpeed = 1.5f;

    [Header("Debug")]
    public KeyCode toggleKey = KeyCode.E;

    private bool atEndPosition = false;
    private bool isMoving = false;
    private Vector3 platformDelta;

    void Start()
    {
        // If startYPos is 0, assume current position
        if (Mathf.Approximately(startYPos, 0f))
        {
            startYPos = transform.position.y;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            Activate();
        }
    }

    IEnumerator MoveToY(float targetY)
    {
        isMoving = true;

        while (Mathf.Abs(transform.position.y - targetY) > 0.01f)
        {
            Vector3 previousPosition = transform.position;
            Vector3 pos = transform.position;
            pos.y = Mathf.MoveTowards(
                pos.y,
                targetY,
                moveSpeed * Time.deltaTime
            );
            transform.position = pos;
            platformDelta = transform.position - previousPosition;
            MovePlayerWithPlatform();
            yield return null;
        }

        transform.position = new Vector3(
            transform.position.x,
            targetY,
            transform.position.z
        );
        platformDelta = Vector3.zero;
        isMoving = false;
    }
    void MovePlayerWithPlatform()
    {
        Vector3 boxCenter = transform.position + Vector3.up * 1f;
        Vector3 boxSize = new Vector3(0.7f, 0.35f, 0.2f);  

        Collider[] hits = Physics.OverlapBox(boxCenter, boxSize);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                CharacterController controller = hit.GetComponent<CharacterController>();

                if (controller != null)
                {
                    controller.Move(platformDelta);
                }
            }
        }
    }
    public void Activate()
    {
        if (isMoving) return;

        float targetY = atEndPosition ? startYPos : endYPos;
        StartCoroutine(MoveToY(targetY));
        atEndPosition = !atEndPosition;
    }
    
}