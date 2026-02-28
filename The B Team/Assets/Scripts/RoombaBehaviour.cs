using UnityEngine;

public class RoombaBehaviour : MonoBehaviour
{
    public float baseSpeed;
    public float sprintSpeed;
    private float speed;

    public Transform[] patrolPoints;
    public int targetPoint;
    private float targetRotation;
    private bool atTarget = false;

    public SphereCollider collider;
    private int directionMultiplier = 1;
    public float directionChangeBuffer;
    public float directionChangeTimer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetPoint = 1;
        speed = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position == patrolPoints[targetPoint].position)
        {
            increaseTargetInt();
        }

        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[targetPoint].position, speed * Time.deltaTime);
        transform.LookAt(patrolPoints[targetPoint].position);

        if(directionChangeTimer > 0)
        {
            speed = sprintSpeed;
            directionChangeTimer = Mathf.Clamp(directionChangeTimer - Time.deltaTime, 0, directionChangeBuffer);
        }
        else
        {
            speed = baseSpeed;
        }

    }

    void increaseTargetInt()
    {
        if (targetPoint + 1 == patrolPoints.Length && directionMultiplier == 1)
        {
            targetPoint = 0;
            return;
        }
        if (targetPoint - 1 == 0 && directionMultiplier == -1)
        {
            targetPoint = 13;
            return;
        }

        targetPoint += directionMultiplier;



    }

    void changeDirection()
    {
        directionMultiplier *= -1;
        directionChangeTimer = directionChangeBuffer;
        increaseTargetInt();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && directionChangeTimer == 0)
        {
            changeDirection();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        for(int i = 0; i < patrolPoints.Length-1; i++)
        {
            Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i + 1].position);
        }

        Gizmos.DrawLine(patrolPoints[patrolPoints.Length-1].position, patrolPoints[0].position);
    }
}
