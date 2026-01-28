using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private GameObject loadingCanvas;

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        // 1. black screen
        if (loadingCanvas != null)
            loadingCanvas.SetActive(true);

        yield return new WaitForSeconds(2f);

        // 2. start loading
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        // 3. loop
        while (!operation.isDone)
        {
            
            yield return null;
        }
    }
}