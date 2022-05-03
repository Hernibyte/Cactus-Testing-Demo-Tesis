using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class h_PlayerInteractionEvent : UnityEvent<h_PlayerInteraction> { }

public class h_PlayerInteraction : MonoBehaviour, IHitable
{
    public h_PlayerInteractionEvent imDie = new h_PlayerInteractionEvent();

    public void Hited(int damage)
    {
        imDie.Invoke(this);
    }
}
