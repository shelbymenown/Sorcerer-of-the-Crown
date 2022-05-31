using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UnlockSpells : MonoBehaviour
{
    private GameObject Slot3;
    private GameObject Slot4;
    private GameObject Slot5;
    private GameObject Slot6;
    string sceneName;

    private void Awake()
    {
        Slot3 = GameObject.FindGameObjectWithTag("HotBar3");
        Slot4 = GameObject.FindGameObjectWithTag("HotBar4");
        Slot5 = GameObject.FindGameObjectWithTag("HotBar5");
        Slot6 = GameObject.FindGameObjectWithTag("HotBar6");
    }

    private void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }

    private void Update()
    {
        CheckSpellUnlocked();
    }

    private void CheckSpellUnlocked()
    {  
        if (sceneName == "Mechanical Dungeon")
        {
            Slot3.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("ItemSprites/" + "Lock");
            Slot3.transform.GetChild(1).gameObject.GetComponent<Text>().enabled = false;
            Slot3.GetComponentInChildren<UIItem>().enabled = false;
            Slot4.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("ItemSprites/" + "Lock");
            Slot4.transform.GetChild(1).gameObject.GetComponent<Text>().enabled = false;
            Slot4.GetComponentInChildren<UIItem>().enabled = false;
            Slot5.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("ItemSprites/" + "Lock");
            Slot5.transform.GetChild(0).gameObject.GetComponent<Text>().enabled = false;
            Slot5.GetComponentInChildren<UIItem>().enabled = false;
            Slot6.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("ItemSprites/" + "Lock");
            Slot6.transform.GetChild(0).gameObject.GetComponent<Text>().enabled = false;
            Slot6.GetComponentInChildren<UIItem>().enabled = false;
        }
        else if (sceneName == "Sand Temple")
        {
            Slot5.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("ItemSprites/" + "Lock");
            Slot5.transform.GetChild(0).gameObject.GetComponent<Text>().enabled = false;
            Slot5.GetComponentInChildren<UIItem>().enabled = false;
            Slot6.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("ItemSprites/" + "Lock");
            Slot6.transform.GetChild(0).gameObject.GetComponent<Text>().enabled = false;
            Slot6.GetComponentInChildren<UIItem>().enabled = false;
        }
    }
}
