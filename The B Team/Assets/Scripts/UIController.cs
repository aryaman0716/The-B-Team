using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UIController : MonoBehaviour
{
    public static bool Paused = false;

    public GameObject[] PauseHide;
    public GameObject[] PauseShow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (Paused)
            {
                UnPauseGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Cursor.visible = true;
        Paused = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0.0f;
        foreach (GameObject thisObject in PauseHide)
        {
            thisObject.SetActive(false);
        }
        foreach (GameObject thisObject in PauseShow) 
        {
            thisObject.SetActive(true);
        }

    }
    public void UnPauseGame()
    {
        Debug.Log("unpause");
        Cursor.visible=false;
        Paused = false;
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        foreach (GameObject thisObject in PauseShow)
        {
            thisObject.SetActive(false);
        }
        foreach (GameObject thisObject in PauseHide)
        {
            thisObject.SetActive(true);
        }
    }

    public void exitGame()
    {
        Paused = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

}
