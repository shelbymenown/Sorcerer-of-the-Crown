using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    void Start()
    {
        Debug.Log("PRELOAD");
        OnLoad();
    }
    public void PlayGame()
    {
        // Load next scene in queue
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGame()
    {

    }

    public void Settings()
    {

    }

    public void ExitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        PlayerPreferences.current.masterVolumeLevel = volume;
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPreferences.current.musicVolumeLevel = volume;
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("Sfx", Mathf.Log10(volume) * 20);
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
            Debug.Log(Application.persistentDataPath + "saves/preferences.dat");
            PlayerPreferences.current = (PlayerPreferences)SerializationManager.Load(Application.persistentDataPath + "/saves/preferences.dat");
            Debug.Log(PlayerPreferences.current);
            Debug.Log(PlayerPreferences.current.masterVolumeLevel);
            // Debug.Log(SaveData.current.preferences.volumeLevel);
            SetVolume(PlayerPreferences.current.masterVolumeLevel);
            SetMusicVolume(PlayerPreferences.current.musicVolumeLevel);
            SetSFXVolume(PlayerPreferences.current.sfxVolumeLevel);
        }
        else
        {
            PlayerPreferences.current.masterVolumeLevel = 0.7f;
            PlayerPreferences.current.musicVolumeLevel = 0.4f;
            PlayerPreferences.current.sfxVolumeLevel = 0.5f;
            OnSave();
            OnLoad();
        }
    }
}
