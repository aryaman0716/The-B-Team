using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string gameStartName;
    //write the file name of the scene you want the button to load into, i think it has to be enabled in the build settings too.

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameStartName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
