using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerUI : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject branchButtons;
    public TextMeshProUGUI dialogFrame;
    public bool dialogueStarted;
    public float textSpeed;
    public NPC_Dialogue dialogueInfo;
    public bool collidingWithNPC;
    bool allBranchesDone;
    bool showBranches;
    bool branchesStarted;
    public int indexPre;
    int indexPost;
    int selectedBranch;

    private void Update()
    {
        if (collidingWithNPC == true && Input.GetKeyDown(KeyCode.E))
        {
            startDialogueSystem();
        }
        if (dialogueStarted == true)
        {
            if (Input.GetKeyUp(KeyCode.End) || Input.GetKeyUp(KeyCode.KeypadEnter))
            {
                dialogFrame.gameObject.SetActive(false);
                dialoguePanel.gameObject.SetActive(false);
            }
            if (Input.GetKeyUp(KeyCode.Return))
            {
                if(dialogueInfo.dialoguePreBranches.Count > indexPre+1)
                {
                    StartCoroutine(ShowText());
                }
                else 
                {
                    showBranches = true;
                }
            }
        }
        if (showBranches == true)
        {
            dialogFrame.SetText("");
            branchButtons.gameObject.SetActive(true);
            branchesStarted = true;
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
        dialoguePanel.gameObject.SetActive(true);
        dialogFrame.gameObject.SetActive(true);
        if (allBranchesDone)
        {
            dialogFrame.SetText(dialogueInfo.allDoneNPCDialogue);
        }
        else 
        {
            if (branchesStarted)
            {
                dialogFrame.SetText("");
                branchButtons.gameObject.SetActive(true);
            }
            else
            {
                indexPre = 0;
                dialogFrame.SetText(dialogueInfo.dialoguePreBranches[indexPre]);
            }
        }
    }


    public void selectBranch(int index)
    {
        StartCoroutine(Selection(index));
    }

    IEnumerator ShowText()
    {
        yield return new WaitForSeconds(0.1f);
        indexPre++;
        dialogFrame.SetText(dialogueInfo.dialoguePreBranches[indexPre]);
    }
    IEnumerator Selection(int index)
    {
        yield return new WaitForSeconds(0.1f);
        selectedBranch = index;
        indexPost = 1;
        showBranches = false;
        branchButtons.gameObject.SetActive(false);
        dialogFrame.SetText(dialogueInfo.NPCDialogBranches[index].branchDialogue[indexPost]);
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
