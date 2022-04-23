using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class h_MenuManager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("h_Game");
    }
}
