using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoSettings : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    public Dropdown qualityDropdown;
    public Dropdown textureDropdown;
    public Dropdown aaDropdown;
    Resolution[] resolutions;
    // private Dictionary<int, Resolution> _resolutions = new Dictionary<int, Resolution>();

    // Start is called before the first frame update
    void Start()
    {
        resolutionDropdown.ClearOptions();
        List<string> resOptions = new List<string>();
        resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;
        int maxRefresh = -1;

        //for (int i = 0; i < resolutions.Length; i++)
        //{
        //    if (resolutions[i].refreshRate > maxRefresh)
        //        maxRefresh = resolutions[i].refreshRate;
        //}

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " " + resolutions[i].refreshRate + "Hz";
            //if (resolutions[i].refreshRate != maxRefresh)
            //    continue;
            resOptions.Add(option);
            // _resolutions.Add(resOptions.Count - 1, resolutions[i]);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }

        resolutionDropdown.AddOptions(resOptions);
        resolutionDropdown.value = resOptions.FindIndex(c => c.Contains(Screen.currentResolution.height.ToString()) && c.Contains(Screen.currentResolution.width.ToString()) && c.Contains(Screen.currentResolution.refreshRate.ToString()));
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetTextureQuality(int textureIndex)
    {
        QualitySettings.masterTextureLimit = textureIndex;
        qualityDropdown.value = 6;
    }

    public void SetAntiAliasing(int aaIndex)
    {
        QualitySettings.antiAliasing = aaIndex;
        qualityDropdown.value = 6;
    }

    public void SetQuality(int qualityIndex)
    {
        if (qualityIndex != 6)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
        }
        switch (qualityIndex)
        {
            case 0: // Very low
                textureDropdown.value = 3;
                aaDropdown.value = 0;
                break;
            case 1: // Low
                textureDropdown.value = 2;
                aaDropdown.value = 0;
                break;
            case 2: // Medium
                textureDropdown.value = 1;
                aaDropdown.value = 0;
                break;
            case 3: // High
                textureDropdown.value = 0;
                aaDropdown.value = 0;
                break;
            case 4: // Very high
                textureDropdown.value = 0;
                aaDropdown.value = 1;
                break;
            case 5: // Ultra
                textureDropdown.value = 0;
                aaDropdown.value = 2;
                break;
        }
        qualityDropdown.value = qualityIndex;
    }
    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
