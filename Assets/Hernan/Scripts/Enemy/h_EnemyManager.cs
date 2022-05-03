using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class h_EnemyManager : MonoBehaviour
{
    [SerializeField] List<Enemy> enemyList;
    [SerializeField] h_UIManager uIManager;
    int enemyAmount;

    void Awake()
    {
        foreach (Enemy enemy in enemyList)
        {
            enemy.imDie.AddListener(DiscountEnemyAmount);
        }
        enemyAmount = enemyList.Count;
    }

    void DiscountEnemyAmount(Enemy enemy)
    {
        enemyAmount--;
        if (enemyAmount <= 0)
        {
            uIManager.ShowYouWin();
        }
        enemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }
}
