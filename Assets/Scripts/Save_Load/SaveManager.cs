using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public TMP_InputField saveName;
    public GameObject loadButtonPrefab;

    public void Save()
    {
        // Create a file or open file to save to
        // Binary Formatter -- allows us to write data to file
        // Serialization method to write to the file
    }

    public void Load()
    {

    }

    //public string[] saveFiles;
    //public void GetLoadFiles()
    //{
    //    if (!Directory.Exists(Application.persistentDataPath + "/saves/"))
    //    {
    //        Directory.CreateDirectory(Application.persistentDataPath + "/saves/");
    //    }

    //    saveFiles = Directory.GetFiles(Application.persistentDataPath + "/saves/");
    //}
}
