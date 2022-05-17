using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class h_GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] h_UIManager uIManager;
    [SerializeField] int chargedShootThornMaxAmount;
    [SerializeField] int flowerShootThornMaxAmount;

    h_PlayerInteraction playerInteraction;
    h_PlayerAttack playerAttack;

    List<GameObject> listChargedShootThorn = new List<GameObject>();
    List<GameObject> listOfFlowerShootThorn = new List<GameObject>();

    void Awake()
    {
        playerInteraction = player.GetComponent<h_PlayerInteraction>();
        playerAttack = player.GetComponent<h_PlayerAttack>();

        playerInteraction.imDie.AddListener(IfPlayerDie);
        playerAttack.shootChargedThorn.AddListener(IfPlayerShootChargedThorn);
        playerAttack.shootFlowerThorn.AddListener(IfPlayerShootFlowerThorn);
    }

    void IfPlayerDie(h_PlayerInteraction player)
    {
        Destroy(player.gameObject);
        uIManager.ShowYouDie();
    }

    void IfPlayerShootChargedThorn(GameObject thorn)
    {
        if (listChargedShootThorn.Count >= chargedShootThornMaxAmount)
        {
            GameObject obj = listChargedShootThorn[0];
            listChargedShootThorn.Remove(obj);
            Destroy(obj);
        }
        
        listChargedShootThorn.Add(thorn);
        thorn.GetComponent<h_ChargedThornBehaviour>().imDie.AddListener(IfChargedThornDie);
    }

    void IfPlayerShootFlowerThorn(GameObject thorn)
    {
        if (listOfFlowerShootThorn.Count >= flowerShootThornMaxAmount)
        {
            GameObject obj = listOfFlowerShootThorn[0];
            listOfFlowerShootThorn.Remove(obj);
            Destroy(obj);
        }

        listOfFlowerShootThorn.Add(thorn);
        thorn.GetComponent<FlowerThornBehaviour>().imDie.AddListener(IfFlowerThornDie);
        thorn.GetComponent<FlowerThornBehaviour>().playerMovement = player.GetComponent<h_PlayerMovement>();
    }

    void IfFlowerThornDie(GameObject thorn) 
    {
        listOfFlowerShootThorn.Remove(thorn);
    }

    void IfChargedThornDie(GameObject thorn)
    {
        listChargedShootThorn.Remove(thorn);
    }
}
