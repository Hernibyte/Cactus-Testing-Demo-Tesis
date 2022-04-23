using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class h_UIManager : MonoBehaviour
{
    [SerializeField] GameObject txt_YouDie;
    [SerializeField] GameObject txt_YouWin;

    void Awake()
    {
        txt_YouDie.SetActive(false);
        txt_YouWin.SetActive(false);
    }

    public void ShowYouDie()
    {
        txt_YouDie.SetActive(true);
    }

    public void ShowYouWin()
    {
        txt_YouWin.SetActive(true);
    }
}
