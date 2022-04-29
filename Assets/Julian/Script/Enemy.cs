﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Enemy : MonoBehaviour
{
    #region Variables
    int health;
    int damage;
    float speed;
    bool detectsPlayer;
    bool fly;
    bool onEnemyDeath;
    enum onDetect { chasesPlayer , flees , standsStill }
    enum flyIdleMovments { standStill, movesSideways, movesUpDown, patroll, moveRandom }
    enum groundIdleMovments { standStill, movesSideways, patroll, moveRandom }
    enum attacksType { mele, shoot}
    enum onDie { explode, generateOilPuddle}
    onDetect enemyAction;
    flyIdleMovments flyIdleEnemy;
    groundIdleMovments groundIdelEnemy;
    attacksType attackType;
    onDie dieActions;
    float detectionRadio;
    bool attack;
    bool dropMoney;
    int moneyDrop;
    bool chanceOfActivateOnDeath;
    float activateChance;
    bool startsGoingRight;
    bool startsGoingUp;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region EnemyInspectorEditor
#if UNITY_EDITOR
    [CustomEditor(typeof(Enemy))]
    public class EnemyEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Enemy enemy = (Enemy)target;
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("------------------------------------Base Details------------------------------------");
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Health", GUILayout.MaxWidth(50));
            enemy.health = EditorGUILayout.IntField(enemy.health);
            EditorGUILayout.LabelField("Speed", GUILayout.MaxWidth(50));
            enemy.speed = EditorGUILayout.FloatField(enemy.speed);
            EditorGUILayout.LabelField("Damage", GUILayout.MaxWidth(50));
            enemy.damage = EditorGUILayout.IntField(enemy.damage);
            EditorGUILayout.EndHorizontal();           
            enemy.fly = EditorGUILayout.Toggle("Enemy flies", enemy.fly);
            if (enemy.fly)
            {
                enemy.flyIdleEnemy = (flyIdleMovments)EditorGUILayout.EnumPopup("Idle Movments", enemy.flyIdleEnemy);
                if(enemy.flyIdleEnemy == flyIdleMovments.movesSideways)
                {
                    enemy.startsGoingRight = EditorGUILayout.Toggle("Starts Going Right", enemy.startsGoingRight);                    
                }
                if (enemy.flyIdleEnemy == flyIdleMovments.movesUpDown)
                {
                    enemy.startsGoingUp = EditorGUILayout.Toggle("Starts Going Up", enemy.startsGoingUp);
                }
            }
            else
            {
                enemy.groundIdelEnemy = (groundIdleMovments)EditorGUILayout.EnumPopup("Idle Movments", enemy.groundIdelEnemy);
                if (enemy.groundIdelEnemy == groundIdleMovments.movesSideways)
                {
                    enemy.startsGoingRight = EditorGUILayout.Toggle("Starts Going Right", enemy.startsGoingRight);
                }
            }
            enemy.dropMoney = EditorGUILayout.Toggle("Enemy drop money", enemy.dropMoney);
            if (enemy.dropMoney)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Money droped", GUILayout.MaxWidth(235));
                enemy.moneyDrop = EditorGUILayout.IntField(enemy.moneyDrop);
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("------------------------------------Behavior Details------------------------------------");
            enemy.detectsPlayer = EditorGUILayout.Toggle("Detects Player", enemy.detectsPlayer);
            if (enemy.detectsPlayer)
            {
                enemy.enemyAction = (onDetect)EditorGUILayout.EnumPopup("Enemy Action", enemy.enemyAction);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Detection Radio", GUILayout.MaxWidth(235));
                enemy.detectionRadio = EditorGUILayout.FloatField(enemy.detectionRadio);
                EditorGUILayout.EndHorizontal();
                enemy.attack = EditorGUILayout.Toggle("Enemy Attacks", enemy.attack);

                if (enemy.attack)
                {
                    enemy.attackType = (attacksType)EditorGUILayout.EnumPopup("Enemy attack Type", enemy.attackType);
                }
            }
            enemy.onEnemyDeath = EditorGUILayout.Toggle("On Enemy Death", enemy.onEnemyDeath);
            if (enemy.onEnemyDeath)
            {
                enemy.dieActions = (onDie)EditorGUILayout.EnumPopup("Enemy attack Type", enemy.dieActions);
                EditorGUILayout.BeginHorizontal();
                enemy.chanceOfActivateOnDeath = EditorGUILayout.Toggle("Chance Of Activate On Death", enemy.chanceOfActivateOnDeath);
                if (enemy.chanceOfActivateOnDeath)
                {
                    EditorGUILayout.LabelField("Percentage", GUILayout.MaxWidth(100));
                    enemy.speed = EditorGUILayout.FloatField(enemy.activateChance);                 
                }
                EditorGUILayout.EndHorizontal();

            }
        }
    }
#endif
    #endregion
}
