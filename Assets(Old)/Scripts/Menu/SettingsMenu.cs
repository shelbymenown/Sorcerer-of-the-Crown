using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Text masterVolumeText;
    public Text musicVolumeText;
    public Text sfxVolumeText;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    public void Awake()
    {
        OnLoad();
    }
    private void Start()
    {
        float volume;
        audioMixer.GetFloat("Master", out volume);
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        masterVolumeText.text = Mathf.FloorToInt(volume * 100) + "%";
        //masterVolumeText.text = (int)((volume + 80) * (float)(5.0 / 4.0)) + "%";
        OnLoad();
    }

    public void SetMasterVolume (float volume)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        masterVolumeText.text = Mathf.FloorToInt(volume * 100) + "%";
        //masterVolumeText.text = (int)((volume + 80) * (float)(5.0 / 4.0)) + "%";
        Debug.Log("Master: " + volume);
        Debug.Log(masterVolumeText.text);
        PlayerPreferences.current.masterVolumeLevel = volume;
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        musicVolumeText.text = Mathf.FloorToInt(volume * 100) + "%";
        //musicVolumeText.text = (int)((volume + 80) * (float)(5.0 / 4.0)) + "%";
        Debug.Log("Music: " + volume);
        PlayerPreferences.current.musicVolumeLevel = volume;
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("Sfx", Mathf.Log10(volume) * 20);
        sfxVolumeText.text = Mathf.FloorToInt(volume * 100) + "%";
        //sfxVolumeText.text = (int)((volume + 80) * (float)(5.0 / 4.0)) + "%";
        Debug.Log("Sfx: " + volume);
        PlayerPreferences.current.sfxVolumeLevel = volume;
    }

    public void OnSave()
    {
        SerializationManager.Save("preferences", PlayerPreferences.current);
    }

    public void OnLoad()
    {
        if (SerializationManager.checkDirectoryExists())
        {
            PlayerPreferences.current = (PlayerPreferences)SerializationManager.Load(Application.persistentDataPath + "/saves/preferences.dat");

            Debug.Log(PlayerPreferences.current);
            Debug.Log(PlayerPreferences.current.masterVolumeLevel);

            SetMasterVolume(PlayerPreferences.current.masterVolumeLevel);
            masterSlider.value = PlayerPreferences.current.masterVolumeLevel;

            SetMusicVolume(PlayerPreferences.current.musicVolumeLevel);
            musicSlider.value = PlayerPreferences.current.musicVolumeLevel;

            SetSFXVolume(PlayerPreferences.current.sfxVolumeLevel);
            sfxSlider.value = PlayerPreferences.current.sfxVolumeLevel;
        }
        else
        {
            Debug.Log("Directory 'saves' does not exist yet");
        }
    }
}
