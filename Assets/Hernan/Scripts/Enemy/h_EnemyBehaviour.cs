using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class h_EnemyBehaviourEvent : UnityEvent<h_EnemyBehaviour> { }
public enum h_EnemyState
{
    Idle,
    Search,
    Attack
}

public class h_EnemyBehaviour : MonoBehaviour, IHitable
{
    public h_EnemyBehaviourEvent imDie = new h_EnemyBehaviourEvent();
    public h_EnemyState state;

    void Update()
    {
        switch(state)
        {
            case h_EnemyState.Idle:
                Idle();
                break;
            case h_EnemyState.Search:
                Search();
                break;
                case h_EnemyState.Attack:
                Attack();
                break;
        }
    }

    void Idle()
    {
        
    }

    void Search()
    {

    }

    void Attack()
    {

    }

    public void Hited(float damage)
    {
        imDie.Invoke(this);
    }
}
