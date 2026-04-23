using UnityEngine;
using UnityEngine.SceneManagement;
public class CutsceneController : MonoBehaviour
{
    public string gameSceneName; 
    public void EndCutscene()
    {
        Debug.Log("Cutscene ended → Loading game scene");
        SceneManager.LoadScene(gameSceneName);
    }

    public void SkipCutscene()
    {
        Debug.Log("Cutscene skipped → Loading game scene");
        SceneManager.LoadScene(gameSceneName);
    }
}