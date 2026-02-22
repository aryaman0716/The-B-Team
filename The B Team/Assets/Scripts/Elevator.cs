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
            Vector3 pos = transform.position;
            pos.y = Mathf.MoveTowards(
                pos.y,
                targetY,
                moveSpeed * Time.deltaTime
            );
            transform.position = pos;

            yield return null;
        }

        transform.position = new Vector3(
            transform.position.x,
            targetY,
            transform.position.z
        );

        isMoving = false;
    }

    public void Activate()
    {
        if (isMoving) return;

        float targetY = atEndPosition ? startYPos : endYPos;
        StartCoroutine(MoveToY(targetY));
        atEndPosition = !atEndPosition;
    }
    
}