using UnityEngine;

public class RoombaBehaviour : MonoBehaviour
{
    public float baseSpeed;
    public float sprintSpeed;
    public float rotateSpeed;
    private float speed;

    public Transform[] patrolPoints;
    public int targetPoint;
    private float targetRotation;
    private bool atTarget = false;

    public SphereCollider collider;
    private int directionMultiplier = 1;
    public float directionChangeBuffer;
    public float directionChangeTimer;
    public GameObject managerKeyPrefab;
    public Transform keySpawnPoint;
    public float puddleCheckDistance = 0.5f;
    public LayerMask pureeLayer;

    private bool isShortCircuited = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetPoint = 1;
        speed = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isShortCircuited) return;  // if the Roomba is short-circuited, we render it malfunctioned

        //RaycastHit hit;
        //if (Physics.Raycast(transform.position, transform.forward, out hit, puddleCheckDistance, pureeLayer))
        //{
        //    ShortCircuit();
        //    return;
        //}

        if (transform.position == patrolPoints[targetPoint].position)
        {
            increaseTargetInt();
        }

        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[targetPoint].position, speed * Time.deltaTime);
        var lookPos = patrolPoints[targetPoint].position - transform.position;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);

        if (directionChangeTimer > 0)
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
            targetPoint = patrolPoints.Length - 1;
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
        if(col.gameObject.tag == "Puree" && !isShortCircuited)
        {
            var distance = Vector3.Distance(transform.position, col.transform.position);
            if(distance > 1) { return; }
            ShortCircuit();
        }
    }
    void ShortCircuit()
    {
        if (isShortCircuited) return; 
        isShortCircuited = true;
        Debug.Log("Roomba short-circuited! Dropping key...");

        ObjectiveManager.Instance.CompleteObjective("Find the key for the manager's office.");
        ObjectiveManager.Instance.SetObjective("Find a way to turn off the security camera.");

        if (managerKeyPrefab != null)
        {
            Vector3 spawnPos = keySpawnPoint != null ? keySpawnPoint.position : transform.position + transform.forward * 0.5f;
            GameObject obj = Instantiate(managerKeyPrefab, spawnPos, Quaternion.identity);
            obj.GetComponent<PlacementEmitter>().previewMeshes[0] = GameObject.Find("managerKeyPreviewMeshSolid").GetComponent<MeshRenderer>();
            obj.GetComponent<PlacementEmitter>().previewHighlight = GameObject.Find("managerKeyPreviewHighlight").GetComponent<MeshRenderer>();
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
