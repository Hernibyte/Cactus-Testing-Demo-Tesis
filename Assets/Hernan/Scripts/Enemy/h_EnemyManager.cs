using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class h_EnemyManager : MonoBehaviour
{
    [SerializeField] List<h_EnemyBehaviour> enemyList;
    int enemyAmount;

    void Awake()
    {
        foreach (h_EnemyBehaviour enemy in enemyList)
        {
            enemy.imDie.AddListener(DiscountEnemyAmount);
        }
        enemyAmount = enemyList.Count;
    }

    void DiscountEnemyAmount(h_EnemyBehaviour enemyBehaviour)
    {
        enemyAmount--;
        if (enemyAmount <= 0)
        {
            Debug.Log("You win!");
        }
        enemyList.Remove(enemyBehaviour);
        Destroy(enemyBehaviour.gameObject);
    }
}
