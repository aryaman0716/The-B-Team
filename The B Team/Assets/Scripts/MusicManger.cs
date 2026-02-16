using UnityEngine;

public class MusicManger : MonoBehaviour
{
    public AudioSource Source;
    public AudioClip PlaceholderMusic;
    public float baseVolume = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       Source.clip = PlaceholderMusic;
       Source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Source.volume = baseVolume * GlobalSettings.MasterVolume * GlobalSettings.MusicVolume;
    }
}
