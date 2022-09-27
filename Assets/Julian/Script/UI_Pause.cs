using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class UI_Pause : MonoBehaviour
{
    bool paused = false;
    bool controllsOpen = false;
    public string actualLvl;
    public GameObject PausePanel;
    public GameObject ControlsPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
        /*
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }*/
    }

    public void Pause()
    {
        if (paused)
        {
            paused = false;
            Time.timeScale = 1;
            PausePanel.SetActive(false);
            CloseControlls();
        }
        else
        {
            paused = true;
            Time.timeScale = 0;
            PausePanel.SetActive(true);
        }
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(actualLvl);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowControlls()
    {
        ControlsPanel.SetActive(true);
    }
    public void CloseControlls()
    {
        ControlsPanel.SetActive(false);
    }
}
