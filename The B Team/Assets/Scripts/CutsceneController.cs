using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI; 

public class CutsceneController : MonoBehaviour
{
    public string gameSceneName;
    public float holdDuration = 2.0f; 

    private float holdTimer = 0f;
    private bool isSkipping = false;

    private Animator anim;
    public GameObject scenetransition;

    void Start()
    {
        anim = GameObject.Find("SkipMask").GetComponent<Animator>();
        if (anim != null) { anim.speed = 1 / holdDuration; }
    }
    void Update()
    {
        // Left Control HOLD?
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (anim != null) { anim.SetBool("skipping", true); }
            holdTimer += Time.deltaTime;

            // Log Debug
            if (holdTimer > 0 && !isSkipping)
            {
                Debug.Log($"Hold to skip: {holdTimer:F1} / {holdDuration}");
            }

            // Hold
            if (holdTimer >= holdDuration && !isSkipping)
            {
                isSkipping = true;
                SkipCutscene();
            }
        }
        else
        {
            // No Hold
            if (anim != null) { anim.SetBool("skipping", false); }
            holdTimer = 0f;
        }
    }

    public void EndCutscene()
    {
        Debug.Log("Cutscene ended → Loading game scene");
        StartCoroutine(PlayAnimationAndLoad());
    }

    public void SkipCutscene()
    {
        Debug.Log("Cutscene skipped (Hold Key) → Loading game scene");
        StartCoroutine(PlayAnimationAndLoad());
    }
    public IEnumerator PlayAnimationAndLoad()
    {
        
        Instantiate(scenetransition).GetComponent<sceneTransition>().BeginTransition(gameSceneName);
        yield return new WaitForSeconds(1.5f);

       
    }
    public void EndCutScene()
    {
        StartCoroutine(PlayAnimationAndLoad());
    }
}