using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class h_EnemyManager : MonoBehaviour
{
    [SerializeField] List<h_EnemyBehaviour> enemyList;
    [SerializeField] h_UIManager uIManager;
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
            uIManager.ShowYouWin();
        }
        enemyList.Remove(enemyBehaviour);
        Destroy(enemyBehaviour.gameObject);
    }
}
