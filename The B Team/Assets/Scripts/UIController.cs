using System;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static bool Paused = false;

    public GameObject[] PauseHide;
    public GameObject[] PauseShow;
    public Animator pauseAnimator;
    public GameObject scenetransition;
    public Animator animatorFade;
    public bool menu = false;
    public Button[] menuButtons;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            PauseButton();
        }
    }

    public void PauseGame()
    {
        if (menu) foreach (Button butt in menuButtons) { butt.gameObject.GetComponent<Image>().raycastTarget = false; }


        //Cursor.visible = true;
        animatorFade.SetBool("Faded", true);
        Paused = true;
        if (!menu) Cursor.lockState = CursorLockMode.None;
        if (!menu) Time.timeScale = 0.0f;
        pauseAnimator.SetTrigger("Open");
        foreach (GameObject thisObject in PauseHide)
        {
            thisObject.SetActive(false);
        }
        foreach (GameObject thisObject in PauseShow) 
        {
            
            //thisObject.SetActive(true);
        }
    }
    public void UnPauseGame()
    {
        if (menu) foreach (Button butt in menuButtons) { butt.gameObject.GetComponent<Image>().raycastTarget = true; }
        animatorFade.SetBool("Faded", false);
        Debug.Log("unpause");
        Cursor.visible=false;
        Paused = false;
        if (!menu) Time.timeScale = 1.0f;
        if (!menu) Cursor.lockState = CursorLockMode.Locked;
        pauseAnimator.SetTrigger("Close");
        foreach (GameObject thisObject in PauseShow)
        {
            
            //thisObject.SetActive(false);
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
        Instantiate(scenetransition).GetComponent<sceneTransition>().BeginTransition("MainMenu");
    }

    public void PauseButton()
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
