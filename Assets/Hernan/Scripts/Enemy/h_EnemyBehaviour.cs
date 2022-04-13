using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class h_EnemyBehaviour : MonoBehaviour, IHitable
{
    public void Hited()
    {
        Destroy(gameObject);
    }
}
