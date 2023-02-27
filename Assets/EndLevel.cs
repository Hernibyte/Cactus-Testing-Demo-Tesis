using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

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
    public UnityEvent endLvLCallback = new UnityEvent();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            endLvLCallback.Invoke();
            endLvlPanel.SetActive(true);
            Time.timeScale = 0;
            if (finalTime != null)
            finalTime.SetText("Final Time: " + LvlM.min.ToString() + ":" + LvlM.timer.ToString("F2")) ;
            if (LvlM.npcInLvl)
            {
                images[5].sprite = LvlM.npcImage;
                if (LvlM.talkToNPC)
                {
                    images[5].color = Color.white;
                }
            }
            for (int i = 0; i < LvlM.notCollectedColectables.Count; i++)
            {
                images[i].sprite = LvlM.notCollectedColectables[i];
            }
            for (int i = 0; i < LvlM.collectedColectables.Count; i++)
            {
                images[LvlM.notCollectedColectables.Count + i].sprite = LvlM.collectedColectables[i];
                images[LvlM.notCollectedColectables.Count + i].color = Color.white;
            } 
        }
        
    }
}
