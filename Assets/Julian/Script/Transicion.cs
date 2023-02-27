using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transicion : MonoBehaviour
{
    public string nextLevel;
    public float timeToChange;
    //
    public void Start()
    {
        Time.timeScale = 1;
    }
    private void Update()
    {
        timeToChange -= Time.deltaTime;
        if (timeToChange <= 0)
        { 
            SceneManager.LoadScene(nextLevel);
        }

    }
}
