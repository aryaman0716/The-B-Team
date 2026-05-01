using UnityEngine;
using System.Collections;

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

    public Material[] roomba_materials;
    public MeshRenderer roomba_meshRenderer;
    public AudioClip[] roomba_ClipsScared;
    public AudioClip roomba_ClipDie;
    public GameObject deathParticles;
    public GameObject sparkParticles;
    public Animator roomba_Animator;
    public AudioSource roomba_AudioSource;

    private bool soundPlayed;

    private bool isShortCircuited = false;
    public bool isScared = false;

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
            if (!soundPlayed) 
            {
                soundPlayed = true;
                roomba_AudioSource.PlayOneShot(roomba_ClipsScared[Random.Range(0, roomba_ClipsScared.Length)], GlobalSettings.SFXVolume * roomba_AudioSource.volume);
            }
            isScared = true;
            speed = sprintSpeed;
            directionChangeTimer = Mathf.Clamp(directionChangeTimer - Time.deltaTime, 0, directionChangeBuffer);
        }
        else
        {
            soundPlayed = false;
            isScared = false;
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
            StartCoroutine(ShortCircuit());
        }
    }
    public IEnumerator ShortCircuit()
    {
        if (isShortCircuited) yield break;
        isShortCircuited = true;
        Instantiate(sparkParticles, transform.position, Quaternion.identity);
        roomba_AudioSource.Stop();
        roomba_AudioSource.PlayOneShot(roomba_ClipDie, GlobalSettings.SFXVolume * roomba_AudioSource.volume);
        roomba_Animator.SetTrigger("die");
        yield return new WaitForSeconds(1f);
        Instantiate(deathParticles, transform.position, Quaternion.identity);

        Debug.Log("Roomba short-circuited! Dropping key...");

        ObjectiveManager.Instance.CompleteObjective("Find the key for the manager's office.", "Find a way to turn off the security camera.");

        if (managerKeyPrefab != null)
        {
            Vector3 spawnPos = keySpawnPoint != null ? keySpawnPoint.position : transform.position + transform.forward * 0.5f;
            GameObject obj = Instantiate(managerKeyPrefab, spawnPos, Quaternion.identity);
            obj.GetComponent<PlacementEmitter>().previewMeshes[0] = GameObject.Find("managerKeyPreviewMeshSolid").GetComponent<MeshRenderer>();
            obj.GetComponent<PlacementEmitter>().previewHighlight = GameObject.Find("managerKeyPreviewHighlight").GetComponent<MeshRenderer>();
        }
        yield return null;
    }

    public int GetCurrentState()
    {
        if (isShortCircuited) { return 2; }
        if (isScared) { return 1; }
        return 0;
    }

    private void OnDrawGizmosSelected()
    {
        if (patrolPoints[0] == null) { return; }
        Gizmos.color = Color.blue;

        for(int i = 0; i < patrolPoints.Length-1; i++)
        {

            Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i + 1].position);
        }

        Gizmos.DrawLine(patrolPoints[patrolPoints.Length-1].position, patrolPoints[0].position);
    }
}
