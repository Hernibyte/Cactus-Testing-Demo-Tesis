using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class h_GameManager : MonoBehaviour
{
    [SerializeField] h_PlayerInteraction player;
    [SerializeField] h_UIManager uIManager;

    void Awake()
    {
        player.imDie.AddListener(IfPlayerDie);
    }

    void IfPlayerDie(h_PlayerInteraction player)
    {
        Destroy(player.gameObject);
        uIManager.ShowYouDie();
    }
}
