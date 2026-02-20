using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GlobalSettings : MonoBehaviour
{
    public static float MasterVolume = 1f;
    public static float MusicVolume = 0.5f;
    public static float SFXVolume = 1f;

    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider SFXSlider;

    public bool fullScreen = true;

    private int selectedRes;

    public List<ResolutionItem> resolutions = new List<ResolutionItem>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public TMP_Text resolutionLabel;
    void Start()
    {

    }

    private void Awake()
    {
        MasterSlider.value = MasterVolume;
        MusicSlider.value = MusicVolume;
        SFXSlider.value = SFXVolume;
    }

    // Update is called once per frame
    void Update()
    {
        Screen.SetResolution(resolutions[selectedRes].horizontal, resolutions[selectedRes].vertical, fullScreen);
    }

    public void MasterVolumeSlider(float sliderValue)
    {
        MasterVolume = sliderValue;
        Debug.Log(MasterVolume);
    }
    public void MusicVolumeSlider(float sliderValue)
    {
        MusicVolume = sliderValue;
    }
    public void SFXVolumeSlider(float sliderValue)
    {
        SFXVolume = sliderValue;
    }

    public void ResLeft()
    {
        selectedRes--;
        if(selectedRes < 0)
            selectedRes = 0;

        UpdateResLabel();
    }
    public void ResRight()
    {
        selectedRes++;
        if (selectedRes > resolutions.Count-1)
            selectedRes = resolutions.Count-1;
        UpdateResLabel();
    }

    public void UpdateResLabel()
    {
        resolutionLabel.text = resolutions[selectedRes].horizontal.ToString() + " X " + resolutions[selectedRes].vertical.ToString();
    }

    public void updateFullscreen(bool fullscreen)
    {
        fullScreen = fullscreen;
    }
}

[System.Serializable]
public class ResolutionItem
{
    public int horizontal, vertical;
}
