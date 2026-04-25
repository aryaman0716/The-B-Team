using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManger : MonoBehaviour
{
    public static MusicManger Instance;

    public AudioSource Source;
    public AudioClip[] Music;
    public float baseVolume = 0.5f;


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


    void Update()
    {
        Source.volume = baseVolume * GlobalSettings.MasterVolume * GlobalSettings.MusicVolume;

        switch (SceneManager.GetActiveScene().name)
        {
            case "Room1Blockout":
                {
                    if (Source.clip != Music[0])
                    {
                        Source.clip = Music[0];
                        Source.Play();
                    }
                    break;
                }
            case "Cutscene":
                {
                    if (Source.clip != Music[1])
                    {
                        Source.clip = Music[1];
                        Source.Play();
                    }
                    break;
                }
            case "MainMenu":
                {
                    if (Source.clip != Music[1])
                    {
                        Source.clip = Music[1];
                        Source.Play();
                    }
                    break;
                }
            case "Cutscene End":
                {
                    if (Source.clip != Music[1])
                    {
                        Source.clip = Music[1];
                        Source.Play();
                    }
                    break;
                }
        }
    }
}