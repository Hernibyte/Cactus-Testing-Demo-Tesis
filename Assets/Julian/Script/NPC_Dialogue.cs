using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Dialogue : MonoBehaviour
{
    [System.Serializable]
    [SerializeField]
    public struct dialogueBranch
    {
        public List<string> branchDialogue;
    }

    public bool branch1Done;
    public bool branch2Done;
    public bool branch3Done;
    public List<string> dialoguePreBranches;
    public List<dialogueBranch> NPCDialogBranches;
    public string allDoneNPCDialogue;

}
