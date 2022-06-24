using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetResponse : MonoBehaviour
{
    public PlayerUI pui;
    public TextMeshProUGUI text;
    public int index;
    void Update()
    {
        text.text = pui.dialogueInfo.NPCDialogBranches[index].branchDialogue[0];
    }

}
