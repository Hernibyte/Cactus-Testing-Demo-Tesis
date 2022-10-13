using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class h_MenuManager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("level tutorial");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Menu()
    {
        SceneManager.LoadScene("h_Menu");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
