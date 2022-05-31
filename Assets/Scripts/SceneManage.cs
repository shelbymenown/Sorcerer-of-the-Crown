using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SceneManage : MonoBehaviour
{
    /// <summary>
    /// Go to the scene that is specified by level number. 
    /// Loads saved player preference settings.
    /// </summary>
    /// <param name="level">Level Number (Scene build number)</param>
    public static void LoadLevel(int level)
    {
        if (Time.timeScale == 0f)
            Time.timeScale = 1f;
        SceneManager.LoadScene(level);
    }

    /// <summary>
    /// Loads next scene based on build number.
    /// </summary>
    public static void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
