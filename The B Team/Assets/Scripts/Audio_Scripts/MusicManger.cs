using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MusicManger : MonoBehaviour
{
    public static MusicManger Instance;

    public AudioSource Source;
    public AudioClip[] Music;
    public float baseVolume = 0.5f;
    public static bool phase2 = false;
    public Animator logoAnim;
    public PlayableDirector director;
    public float lastDirTime;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    void LateUpdate()
    {
        Source.volume = baseVolume * GlobalSettings.MasterVolume * GlobalSettings.MusicVolume;
        if (director != null)lastDirTime = (float)director.time;
        switch (SceneManager.GetActiveScene().name)
        {
            case "Room1Blockout":
            case "Cutscene End":
                {
                    if (!phase2)
                    {
                        if (Source.clip != Music[3])
                        {
                            Source.clip = Music[3];
                            Source.Play();
                        }
                        break;
                    }
                    else
                    {
                        if (Source.clip != Music[2] && Source.clip != Music[0])
                        {
                            Source.clip = Music[2];
                            Source.Play();
                            StartCoroutine(LoadTrack());
                        }
                        break;
                    }

                    
                    
                }
            case "Cutscene":
                {
                    if (Source.clip != Music[1])
                    {
                        Source.clip = Music[1];
                        Source.time = lastDirTime + 0.05f ;
                        Source.Play();
                    }
                    break;
                }
            case "MainMenu":
                {
                    if (Source.clip != Music[1])
                    {
                        if (director != null) {director.Play();}
                    }
                    break;
                }
            
        }
    }
    public IEnumerator LoadTrack()
    {
        yield return new WaitForSeconds(28f);
        Source.clip = Music[0];
        Source.Play();
    }
}