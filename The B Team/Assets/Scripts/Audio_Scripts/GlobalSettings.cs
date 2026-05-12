using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GlobalSettings : MonoBehaviour
{
    public static float MasterVolume = 1f;
    public static float MusicVolume = 0.5f;
    public static float SFXVolume = 1f;
    public static float textSpeed = 1f;

    public static bool HeadBob = true;

    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider SFXSlider;

    public bool fullScreen = true;
    public int selectedRes = 0;
    public int selectedTxtSpd = 0;

    public List<ResolutionItem> resolutions = new List<ResolutionItem>();
    public List<float> textSpeeds = new List<float>();
    public List<string> textSpeedLabels = new List<string>();

    public TMP_Text resolutionLabel;
    public TMP_Text textSpeedLabel;

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
        UpdateTextSpeed();
    }

    void Start()
    {

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

    public void textSpeedRight()
    {
        selectedTxtSpd++;

        if (selectedTxtSpd > textSpeeds.Count - 1)
        {
            selectedTxtSpd = textSpeeds.Count - 1;
        }

        UpdateTextSpeed();
        
    }
    public void textSpeedLeft()
    {
        selectedTxtSpd--;

        if (selectedTxtSpd < 0)
        {
            selectedTxtSpd = 0;
        }

        UpdateTextSpeed();
    }
    public void UpdateTextSpeed()
    {
        textSpeed = textSpeeds[selectedTxtSpd];
        textSpeedLabel.text = textSpeedLabels[selectedTxtSpd];
        
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