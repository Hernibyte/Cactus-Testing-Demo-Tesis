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
    bool brancheSelected;
    public int indexPre;
    int indexPost;
    int selectedBranch;


    h_GameManager gm;
    h_PlayerAttack pa;
    public GameObject trampolinAvailable;
    public GameObject trampolin;
    public GameObject pinchPlatformAvailable;
    public TextMeshProUGUI pinchPlatformCuantity;

    private void Start()
    {
        brancheSelected = false;
    }
    private void Update()
    {
        gm = FindObjectOfType<h_GameManager>();
        pa = FindObjectOfType<h_PlayerAttack>();

        pinchPlatformCuantity.text = gm.getChargedShootThornMaxAmount().ToString();
        if (pa.getChargedRangeAttackAvailable())
        {
            pinchPlatformAvailable.SetActive(false);
        }
        else
        {
            pinchPlatformAvailable.SetActive(true);
        }
        if (pa.flowerPowerUpObtained)
        {
            trampolin.SetActive(true);
            if (pa.getFlowerAvailable())
            {
                trampolinAvailable.SetActive(false);
            }
            else
            {
                trampolinAvailable.SetActive(true);
            }
        }


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
                if (dialogueInfo.dialoguePreBranches.Count > indexPre + 1 && branchesStarted == false)
                {
                    StartCoroutine(ShowTextPreBranch());
                }
                else
                {
                    showBranches = true;
                }
                if (brancheSelected)
                {
                    if (dialogueInfo.NPCDialogBranches[selectedBranch].branchDialogue.Count > indexPost +1)
                    { 
                    StartCoroutine(ShowTextPostBranch());
                    }
                    else 
                    {
                        switch (selectedBranch)
                        {
                            case 0:
                                dialogueInfo.branch1Done = true;
                                break;
                            case 1:
                                dialogueInfo.branch2Done = true;
                                break;
                            case 2:
                                dialogueInfo.branch3Done = true;
                                break;
                            default:
                                break;
                        }
                        indexPost = 0;
                        brancheSelected = false;
                    }
                }
                if (dialogueInfo.branch1Done && dialogueInfo.branch2Done && dialogueInfo.branch3Done)
                {
                    allBranchesDone = true;
                }
                if (allBranchesDone)
                {
                    showBranches = false;
                    branchesStarted = false;
                    dialogFrame.SetText(dialogueInfo.allDoneNPCDialogue);
                }

            }
        }
        if (showBranches == true && brancheSelected == false)
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

    IEnumerator ShowTextPreBranch()
    {
        yield return new WaitForSeconds(0.1f);
        indexPre++;
        dialogFrame.SetText(dialogueInfo.dialoguePreBranches[indexPre]);
    }
    IEnumerator ShowTextPostBranch()
    {
        yield return new WaitForSeconds(0.1f);
        indexPost++;
        dialogFrame.SetText(dialogueInfo.NPCDialogBranches[selectedBranch].branchDialogue[indexPost]);
    }
    IEnumerator Selection(int index)
    {
        yield return new WaitForSeconds(0.1f);
        selectedBranch = index;
        indexPost = 1;
        showBranches = false;
        branchButtons.gameObject.SetActive(false);
        dialogFrame.SetText(dialogueInfo.NPCDialogBranches[index].branchDialogue[indexPost]);
        brancheSelected = true;
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
