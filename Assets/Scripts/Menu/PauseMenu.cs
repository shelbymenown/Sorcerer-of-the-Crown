using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class PauseMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot unpaused;

    public static bool GameIsPaused = false;
    [SerializeField] protected GameObject pauseMenuUI;
    [SerializeField] protected GameObject optionsMenuUI;
    [SerializeField] protected GameObject soundMenuUI;
    [SerializeField] protected GameObject controlsMenuUI;
    [SerializeField] protected GameObject saveMenuUI;
    [SerializeField] protected GameObject videoMenuUI;
    [SerializeField] protected GameObject gameOverUI;
    SettingsMenu settingsScript;
    private bool _gameOver = false;
    private GameObject _player;
    private PlayerController controller;
    private GameObject _hotBar;
    private toolbarSelectionIndicator toolSelect;
    private bool _timeScaleAlreadyZero = false; 

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        if (_player != null)
            controller = _player.GetComponent<PlayerController>();
        _hotBar = GameObject.FindGameObjectWithTag("HotBar");
        if (_hotBar != null)
            toolSelect = _hotBar.GetComponent<toolbarSelectionIndicator>();
    }
    private void Start()
    {
        OnPreferenceLoad();
        settingsScript = soundMenuUI.GetComponent<SettingsMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_gameOver)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        soundMenuUI.SetActive(false);
        controlsMenuUI.SetActive(false);
        saveMenuUI.SetActive(false);
        videoMenuUI.SetActive(false);
        settingsScript.OnSave();
        if (!_timeScaleAlreadyZero)
        {
            Time.timeScale = 1f;
        }
        GameIsPaused = false;
        Lowpass();
        _timeScaleAlreadyZero = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        if (Time.timeScale == 0f)
            _timeScaleAlreadyZero = true;
        Time.timeScale = 0f;
        GameIsPaused = true;
        Lowpass();
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
        GameIsPaused = false;
        Lowpass();
    }

    void Lowpass()
    {
        if (Time.timeScale == 0)
            paused.TransitionTo(.001f);
        else
            unpaused.TransitionTo(.001f);
    }

    public void DisableGameOver()
    {
        gameOverUI.SetActive(false);
        GameIsPaused = false;
        //Time.timeScale = 1f;
        Lowpass();
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        if (controller != null)
            controller.enabled = false;
        if (toolSelect != null)
            toolSelect.enabled = false;
        _gameOver = true;
        //Time.timeScale = 0f;
        GameIsPaused = true;
        Lowpass();
    }
    private void OnPreferenceLoad()
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
            Debug.Log("Directory 'saves' does not exist yet");
        }
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
}
