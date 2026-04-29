using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GlobalSettings : MonoBehaviour
{
    public static float MasterVolume = 1f;
    public static float MusicVolume = 0.5f;
    public static float SFXVolume = 1f;

    public static bool HeadBob = true;

    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider SFXSlider;

    public bool fullScreen = true;
    public bool debugObjectLabels = true;
    public int selectedRes = 0;

    public List<ResolutionItem> resolutions = new List<ResolutionItem>();

    public TMP_Text resolutionLabel;

    void Awake()
    {
        if (selectedRes == 0)
        {
            selectedRes = resolutions.Count - 2;
        }

        MasterSlider.value = MasterVolume;
        MusicSlider.value = MusicVolume;
        SFXSlider.value = SFXVolume;

        ApplyResolution();
        UpdateResLabel();
    }

    void Start()
    {
        updateObjectLabels(debugObjectLabels);
    }

    public void MasterVolumeSlider(float sliderValue)
    {
        MasterVolume = sliderValue;
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

        if (selectedRes < 0)
        {
            selectedRes = 0;
        }

        UpdateResLabel();
    }

    public void ResRight()
    {
        selectedRes++;

        if (selectedRes > resolutions.Count - 1)
        {
            selectedRes = resolutions.Count - 1;
        }

        UpdateResLabel();
    }

    public void UpdateResLabel()
    {
        resolutionLabel.text =
            resolutions[selectedRes].horizontal + " X " +
            resolutions[selectedRes].vertical;

        ApplyResolution();
    }

    public void updateFullscreen(bool fullscreenValue)
    {
        fullScreen = fullscreenValue;
        ApplyResolution();
    }

    public void updateBobbing(bool bob)
    {
        HeadBob = bob;
    }

    public void updateObjectLabels(bool val)
    {
        var labels = GameObject.FindGameObjectsWithTag("ObjectLabels");

        if (labels == null)
        {
            return;
        }

        for (int i = 0; i < labels.Length; i++)
        {
            labels[i].SetActive(val);
        }
    }

    void ApplyResolution()
    {
        int w = resolutions[selectedRes].horizontal;
        int h = resolutions[selectedRes].vertical;

        if (fullScreen)
        {
            Screen.SetResolution(w, h, FullScreenMode.FullScreenWindow);
        }
        else
        {
            Screen.SetResolution(w, h, FullScreenMode.Windowed);
        }
    }
}

[System.Serializable]
public class ResolutionItem
{
    public int horizontal;
    public int vertical;
}