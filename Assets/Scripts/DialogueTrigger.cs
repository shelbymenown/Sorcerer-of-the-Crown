using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueTrigger : MonoBehaviour
{

    public Dialogue dialogue;

    public static bool endConversation = false;

    [SerializeField] private Image customimage;
    [SerializeField] private Image buttonImage;
    [SerializeField] private Text nameText;
    [SerializeField] private Text dialogueText;
    [SerializeField] private Button Continue;
    [SerializeField] private Text buttonText;

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("here");
            Activator(true);
            TriggerDialogue();
            /*Time.timeScale = 0f;
            customimage.enabled = true;
            Continue.enabled = true;
            TriggerDialogue();
            buttonImage.enabled = true;
            buttonText.enabled = true;
            nameText.enabled = true;
            dialogueText.enabled = true;
            Destroy(gameObject, 4);*/
            //gameObject.GetComponent<BoxCollider2D>().enabled = false;
            /*if(endConversation)
            {
                DialogueManager.EndDialogue();
                Time.timeScale = 1f;
                customimage.enabled = false;
                Continue.enabled = false;
                buttonText.enabled = false;
                nameText.enabled = false;
                dialogueText.enabled = false;
            }*/
        }
    }

    public void Activator(bool isActive)
    {
        Time.timeScale = isActive ? 0f : 1f;
        customimage.enabled = isActive;
        Continue.enabled = isActive;
        if (isActive)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = !isActive;
        }
        buttonImage.enabled = isActive;
        buttonText.enabled = isActive;
        nameText.enabled = isActive;
        dialogueText.enabled = isActive;
        //gameObject.GetComponent<BoxCollider2D>().enabled = !isActive;

    }

    public void TriggerDialogue ()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

}
