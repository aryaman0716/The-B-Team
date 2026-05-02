using System.Collections;
using System.Data.Common;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class MusicManger : MonoBehaviour
{
    public static MusicManger Instance;

    public AudioSource Source, dirSource;
    public AudioClip[] Music;
    public float baseVolume = 0.5f;
    public static bool phase2 = false;
    public Animator logoAnim;
    public PlayableDirector director;
    public float lastDirTime;
    public float volMod = 1;

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
        Source.volume = baseVolume * GlobalSettings.MasterVolume * GlobalSettings.MusicVolume * volMod;
        dirSource.volume = baseVolume * GlobalSettings.MasterVolume * GlobalSettings.MusicVolume;
        if (director != null)lastDirTime = (float)director.time;
        switch (SceneManager.GetActiveScene().name)
        {
            case "Room1Blockout":
            case "Cutscene End":
                {
                    volMod = 1;
                    if (!phase2)
                    {
                        if (Source.clip != Music[3])
                        {
                            Source.clip = Music[3];
                            StopAllCoroutines();
                            Source.Play();
                        }
                        break;
                    }
                    else
                    {
                        if (Source.clip != Music[2] && Source.clip != Music[0])
                        {
                            Source.clip = Music[2];
                            StopAllCoroutines();
                            Source.Play();
                            StartCoroutine(LoadTrack());
                        }
                        break;
                    }

                    
                    
                }
            case "Cutscene":
                {
                    volMod = 1;
                    if (Source.clip != Music[1])
                    {
                        Source.clip = Music[1];
                        StopAllCoroutines();
                        Source.Play();
                    }
                    else if (!Source.isPlaying)
                    {
                        StopAllCoroutines();
                        Source.Play();
                    }
                        
                      break;
                }
            case "MainMenu":
                {
                    if (Source.clip != Music[1])
                    {
                        director = GameObject.Find("Logo").GetComponent<PlayableDirector>();

                        TimelineAsset timeline = (TimelineAsset)director.playableAsset;

                        foreach (var track in timeline.GetOutputTracks())
                        {
                            if (track is AudioTrack)
                            {
                                director.SetGenericBinding(track, dirSource);
                            }
                        }

                        Source.clip = Music[1];
                        if (director != null)
                        {
                            director.time = 0;
                            director.Play();  
                            volMod = 0;
                            Source.volume = baseVolume * GlobalSettings.MasterVolume * GlobalSettings.MusicVolume * volMod;
                            Source.Play();
                            Source.time = 0;
                        }
                        else
                        {
                            Source.Play();
                            volMod = 1;
                        }
                    }
                    break;
                }
        }
    }
    public IEnumerator LoadTrack()
    {
        yield return new WaitForSeconds(28f);
        Source.clip = Music[0];
        StopAllCoroutines();
        Source.Play();
    }
}