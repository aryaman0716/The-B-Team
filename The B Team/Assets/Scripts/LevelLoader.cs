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
        if (loadingCanvas != null)
            loadingCanvas.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        yield return operation;

        // After scene loads, reposition player
        GameObject spawn = GameObject.Find("PlayerSpawn");

        if (spawn != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                CharacterController cc = player.GetComponent<CharacterController>();
                cc.enabled = false;
                player.transform.position = spawn.transform.position;
                player.transform.rotation = spawn.transform.rotation;
                cc.enabled = true;
            }
        }

        if (loadingCanvas != null)
            loadingCanvas.SetActive(false);
    }
}