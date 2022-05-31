using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class PauseMenu : MonoBehaviour
{
    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot unpaused;

    public static bool GameIsPaused = false;
    [SerializeField] protected GameObject pauseMenuUI;
    [SerializeField] protected GameObject optionsMenuUI;
    [SerializeField] protected GameObject soundMenuUI;
    [SerializeField] protected GameObject controlsMenuUI;
    [SerializeField] protected GameObject saveMenuUI;
    [SerializeField] protected GameObject videoMenuUI;
    SettingsMenu settingsScript;

    private void Start()
    {
        settingsScript = soundMenuUI.GetComponent<SettingsMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        Time.timeScale = 1f;
        GameIsPaused = false;
        Lowpass();
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Lowpass();
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
        Lowpass();
    }

    void Lowpass()
    {
        if (Time.timeScale == 0)
            paused.TransitionTo(.001f);
        else
            unpaused.TransitionTo(.001f);
    }

}
