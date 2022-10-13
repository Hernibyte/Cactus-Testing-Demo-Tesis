using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndLevel : MonoBehaviour
{
    GameObject endLvlPanel;
    public TextMeshProUGUI finalTime;
    public LevelManager LvlM;
    public GameObject talkToNpcDone;
    public GameObject talkToNpcFail;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            finalTime.SetText(LvlM.min.ToString() + " : " + LvlM.timer.ToString("F2"));
        }
        if (LvlM.npcInLvl)
        {
            if (LvlM.talkToNPC)
            {
                talkToNpcDone.SetActive(true);
            }
            else
            {
                talkToNpcFail.SetActive(true);
            }
        }
    }
}
