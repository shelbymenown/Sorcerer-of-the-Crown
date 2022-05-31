using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class goToNextScene : MonoBehaviour
{
    PlayerSaveManager saveManger;
    public TMP_Text m_Text;
    public Button m_Button;
    public string SceneToLoad;
    private bool isPressed = false;
    private AsyncOperation asyncOperation;

    private void Awake()
    {
        saveManger = GetComponent<PlayerSaveManager>();
    }

    void Start()
    {
        //Start loading the Scene asynchronously and output the progress bar
        StartCoroutine(LoadScene());
    }

    public void TaskOnClick()
    {
        if (asyncOperation.progress >= 0.9f)
        {
            isPressed = true;
        }
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(0.2f);

        //Begin to load the Scene you specify
        asyncOperation = SceneManager.LoadSceneAsync(SceneToLoad);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        Debug.Log("Pro :" + asyncOperation.progress);
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            //Output the current progress
            m_Text.SetText("Loading...");

            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                //Change the Text to show the Scene is ready
                m_Text.SetText("Continue");
                //Wait to you press the space key to activate the Scene
                if (isPressed)
                {
                    //Activate the Scene
                    //saveManger.SetLoadStateTrue(true);
                    asyncOperation.allowSceneActivation = true;
                }
                    
            }

            yield return new WaitForSeconds(0.2f);
        }
    }


}
