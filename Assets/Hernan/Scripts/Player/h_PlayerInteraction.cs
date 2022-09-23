using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class h_PlayerInteractionEvent : UnityEvent<h_PlayerInteraction> { }

public class h_PlayerInteraction : MonoBehaviour, IHitable
{
    public h_PlayerInteractionEvent imDie = new h_PlayerInteractionEvent();

    private h_PlayerAttack playerAttack;
    private h_GameManager gm;

    private void Awake()
    {
        playerAttack = GetComponent<h_PlayerAttack>();
        gm = FindObjectOfType<h_GameManager>();
    }

    public void Hited(int damage)
    {
        imDie.Invoke(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PowerUp")
        {
            if (other.gameObject.name == "Trampolin")
            {
                playerAttack.flowerPowerUpObtained = true;
            }
            if (other.gameObject.name == "PinchoExtra")
            {
                gm.addChargedShootThornMaxAmount();
            }
            Destroy(other.gameObject);
        }
    }
}
