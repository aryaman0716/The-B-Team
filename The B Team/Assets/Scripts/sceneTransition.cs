using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneTransition : MonoBehaviour
{
    public Animator animator;
    private string sceneName;
    public ScreenFade fade;
    public cutoutMask mask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
 
    public void BeginTransition(string targetScene)
    {
        sceneName = targetScene;
        DontDestroyOnLoad(gameObject);
        StartCoroutine(LoadScene(sceneName));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LoadScene(string sceneName)
    {
        animator.SetTrigger("Play");
        if (fade != null)
        {
            yield return fade.FadeOut();
        }
        
        yield return new WaitForSeconds(1.5f);
        animator.speed = 0f;

        //yield return null;
        SceneManager.LoadScene(sceneName);
        

        yield return null;

        Canvas.ForceUpdateCanvases();


        if (mask != null)
        {
            mask.SetAllDirty();
        }
        yield return new WaitForSeconds(0.01f);
        animator.speed = 1f;
        if (fade != null)
        {
            yield return fade.FadeIn();
        }
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}
