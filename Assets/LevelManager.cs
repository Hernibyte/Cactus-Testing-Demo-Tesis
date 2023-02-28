using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public float timer;
    public TextMeshProUGUI actualTime;
    public int min;
    public List<Sprite> collectedColectables;
    public List<Sprite> notCollectedColectables;
    public bool npcInLvl;
    public bool talkToNPC;
    public Sprite npcImage;

    public string lvlName;

    private void Start()
    {
        if (npcInLvl)
        talkToNPC = false;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 60)
        {
            min++;
            timer -= 60;
        }
        actualTime.SetText(min.ToString() + " : " + timer.ToString("F2"));
    }

    public void TimeLess(float t)
    {
        timer -= t;
        if (timer < 0)
        {
            if (min > 0)
            {
                min--;
                float a = timer * -1;
                timer = 60 - a;
            }
            else timer = 0;
        }
    }

    public void SaveLvLRecordTime()
    {
        if (PlayerPrefs.GetFloat(lvlName) > timer + (min * 60) || PlayerPrefs.GetFloat(lvlName) <= 0)
        {
            PlayerPrefs.SetFloat(lvlName, timer + (min * 60));
        }
    }
}
