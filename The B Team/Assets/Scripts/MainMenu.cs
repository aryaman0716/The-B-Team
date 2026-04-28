using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string gameStartName;
    public string gameContinueName;
    public GameObject scenetransition;
    //write the file name of the scene you want the button to load into, i think it has to be enabled in the build settings too.

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        PlayerPrefs.DeleteKey("CheckpointIndex"); // start fresh without any checkpoint progress
        LoadScene(gameStartName);
    }
    public void ContinueGame()
    {
        LoadScene(gameContinueName);
    }
    public void LoadScene(string SceneName)
    {
        Instantiate(scenetransition).GetComponent<sceneTransition>().BeginTransition(SceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
