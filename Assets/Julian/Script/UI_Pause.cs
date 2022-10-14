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
        Time.timeScale = 1;
        SceneManager.LoadScene(actualLvl);
    }
    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("h_Menu");
    }
    public void NextLevel(string LevelName)
    {
        SceneManager.LoadScene(LevelName);

        Time.timeScale = 1;
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
