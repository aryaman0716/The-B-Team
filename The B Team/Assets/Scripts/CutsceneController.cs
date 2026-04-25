using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // สำหรับใช้จัดการ UI Image ถ้าต้องการแสดงแถบ Progress

public class CutsceneController : MonoBehaviour
{
    public string gameSceneName;
    public float holdDuration = 2.0f; // ตั้งค่าเวลาที่ต้องกดค้าง (วินาที)

    private float holdTimer = 0f;
    private bool isSkipping = false;

    private Animator anim;

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
        SceneManager.LoadScene(gameSceneName);
    }

    public void SkipCutscene()
    {
        Debug.Log("Cutscene skipped (Hold Key) → Loading game scene");
        SceneManager.LoadScene(gameSceneName);
    }
}