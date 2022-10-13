using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public float timer;
    public TextMeshProUGUI actualTime;
    public int min;
    public List<GameObject> pastCollectables;
    public List<GameObject> futureCollectables;
    public bool npcInLvl;
    public bool talkToNPC;

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
}
