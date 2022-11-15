using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;
using TMPro;

public class h_MenuManager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(selectedLevel);
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Menu()
    {
        SceneManager.LoadScene("h_Menu");
    }

    public void Options()
    {
        options_Panel.SetActive(!options_Panel.activeSelf);
    }

    public void ChangeLevelSelected(string level)
    {
        selectedLevel = level;
        selectedView.text = level;
    }

    public void MoveImagen(float x)
    {
        selectionImagen.transform.localPosition = new Vector3(x, selectionImagen.transform.localPosition.y);
    }

    public void SelectLevel()
    {
        levelSelector.SetActive(!levelSelector.activeSelf);
    }

    public void ApplyNewRes()
    {
        Resolution newRes;
        bool si = reses.TryGetValue(options_Dropdown.value, out newRes);

        if (si) Screen.SetResolution(newRes.width, newRes.height, true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void Awake()
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

    [SerializeField] private GameObject options_Panel;
    [SerializeField] private GameObject levelSelector;
    [SerializeField] private TMP_Text selectedView;
    [SerializeField] private TMP_Dropdown options_Dropdown;
    private Dictionary<int, Resolution> reses = new Dictionary<int, Resolution>();
    public string selectedLevel = "level tutorial";
    [SerializeField] private GameObject selectionImagen;
}
