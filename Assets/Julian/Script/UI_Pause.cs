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
    public h_CameraController cameraController;
    
    [SerializeField] private GameObject options_Panel;
    [SerializeField] private TMP_Dropdown options_Dropdown;

    public GameObject player;
    public GameObject playerInPause;
    private Dictionary<int, Resolution> reses = new Dictionary<int, Resolution>();

    // Start is called before the first frame update
    void Awake()
    {
        List<string> reses_string = new List<string>();
        int i = 0;

        foreach (Resolution res in Screen.resolutions)
        {
            reses.Add(i, res);
            ++i;

            reses_string.Add(res.width + "x" + res.height);
        }
        options_Dropdown.AddOptions(reses_string);
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
            cameraController.changeZoom();
            player.SetActive(true);
            playerInPause.SetActive(false);
}
        else
        {
            paused = true;
            Time.timeScale = 0;
            PausePanel.SetActive(true);
            cameraController.changeZoom();
            player.SetActive(false);
            playerInPause.SetActive(true);
        }
    }
    
    public void Options()
    {
        options_Panel.SetActive(!options_Panel.activeSelf);
    }

    public void ApplyNewRes()
    {
        Resolution newRes;
        bool si = reses.TryGetValue(options_Dropdown.value, out newRes);

        if (si) Screen.SetResolution(newRes.width, newRes.height, true);
    }
    
    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
