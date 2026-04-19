using UnityEngine;

public class SFXVolume : MonoBehaviour
{
    public float baseVolume = 0.5f;
    public AudioSource audio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audio.volume = baseVolume * GlobalSettings.SFXVolume * GlobalSettings.MasterVolume;
    }
}
