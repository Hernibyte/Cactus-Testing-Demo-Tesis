using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Dialogue : MonoBehaviour
{
    [System.Serializable]
    [SerializeField]
    public struct dialogueBranch
    {
        public bool branchDone;
        public List<string> playerDialog;
        public List<string> NPCDialog;
    }

    public string firstPlayerDialogue;
    public string firstNPCDialogue;
    public List<dialogueBranch> NPCDialogBranches;
    public string allDoneNPCDialogue;

    public void dialogueBranchDone(int index)
    {
        if (NPCDialogBranches.Count > index)
        {
            //NPCDialogBranches[index].branchDone = true;
        }
    }
}
