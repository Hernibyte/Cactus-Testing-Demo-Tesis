using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerUI : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogFrame;
    public bool dialogueStarted;
    public float textSpeed;
    bool playerDialogue;
    private NPC_Dialogue dialogueInfo;
    public bool collidingWithNPC;

    void Start()
    {
        
    }
    private void Update()
    {
        if (collidingWithNPC == true && Input.GetKeyDown(KeyCode.E))
        {
            startDialogueSystem();
        }
        if (dialogueStarted == true)
        {
            dialoguePanel.SetActive(true);
            if (Input.GetKeyUp(KeyCode.End) || Input.GetKeyUp(KeyCode.KeypadEnter))
            {
                dialogFrame.gameObject.SetActive(false);
                dialoguePanel.gameObject.SetActive(false);
            }
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            dialogueStarted = false;
            dialogFrame.gameObject.SetActive(false);
            dialoguePanel.gameObject.SetActive(false);
        }
    }
    void startDialogueSystem()
    {
        dialogueStarted = true;
        dialogFrame.gameObject.SetActive(true);
        dialogFrame.text = dialogueInfo.firstPlayerDialogue;
    }

    void setDialogue(int branchIndex, int dialogueIndex, bool isPlayerDialogue)
    {
        if (isPlayerDialogue)
        {
            //dialogFrame.text = NPCDialogBranches[branchIndex].playerDialog[dialogueIndex];
        }
        else
        {
           // dialogFrame.text = NPCDialogBranches[branchIndex].NPCDialog[dialogueIndex];
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "NPC")
        {
            dialogueInfo = collision.gameObject.GetComponent<NPC_Dialogue>();
            collidingWithNPC = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "NPC")
        {
            dialogueStarted = false;
            dialogFrame.gameObject.SetActive(false);
            dialoguePanel.gameObject.SetActive(false);
            collidingWithNPC = false;
        }
    }

}
