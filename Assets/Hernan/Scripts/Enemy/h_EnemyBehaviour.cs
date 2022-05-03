using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class h_EnemyBehaviourEvent : UnityEvent<h_EnemyBehaviour> { }

public class h_EnemyBehaviour : MonoBehaviour, IHitable
{
    [SerializeField] LayerMask lm_Player;
    public h_EnemyBehaviourEvent imDie = new h_EnemyBehaviourEvent();

    void OnCollisionEnter2D(Collision2D collision)
    {
        IHitable hitable;
        if (collision.gameObject.TryGetComponent<IHitable>(out hitable))
        {
            hitable.Hited(1);
        }
    }

    public void Hited(int damage)
    {
        imDie.Invoke(this);
    }
}
