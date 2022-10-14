﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndLevel : MonoBehaviour
{
    [System.Serializable]
    public struct colectable
    {
        float Id;
        Sprite sprite;
        bool inLevel;
        bool collected;
    }

    public GameObject endLvlPanel;
    public TextMeshProUGUI finalTime;
    public LevelManager LvlM;
    public List<colectable> colectableItems;
    public List<Image> images; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            endLvlPanel.SetActive(true);
            Time.timeScale = 0;
            finalTime.SetText("Final Time: " + LvlM.min.ToString() + ":" + LvlM.timer.ToString("F2")) ;
            if (LvlM.npcInLvl)
            {
                images[5].sprite = LvlM.npcImage;
                if (LvlM.talkToNPC)
                {
                    images[5].color = Color.white;
                }
            }
        }
        
    }
}
