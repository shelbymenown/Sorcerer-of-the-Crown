using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadNextScene : MonoBehaviour
{
    PlayerSaveManager saveManger;
    public string sceneToLoad;

    private void Awake()
    {
        saveManger = GetComponent<PlayerSaveManager>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            saveManger.SetLoadStateTrue(true);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
