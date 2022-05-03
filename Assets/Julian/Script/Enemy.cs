using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Enemy : MonoBehaviour, IHitable
{
    #region Variables
    [SerializeField][HideInInspector]enum onDetect { chasesPlayer, flees, standsStill }
    [SerializeField] [HideInInspector] enum flyIdleMovments { standStill, movesSideways, movesUpDown, patroll, moveRandom }
    [SerializeField] [HideInInspector] enum groundIdleMovments { standStill, movesSideways, patroll, moveRandom }
    [SerializeField] [HideInInspector] enum attacksType { mele, shoot }
    [SerializeField] [HideInInspector] enum onDie { explode, generateOilPuddle }
    [SerializeField] [HideInInspector] onDetect enemyAction;
    [SerializeField] [HideInInspector] flyIdleMovments flyIdleEnemy;
    [SerializeField] [HideInInspector] groundIdleMovments groundIdelEnemy;
    [SerializeField] [HideInInspector] attacksType attackType;
    [SerializeField] [HideInInspector] onDie dieActions;
    [SerializeField] [HideInInspector] GameObject target;
    [SerializeField] [HideInInspector] int health;
    [SerializeField] [HideInInspector] int damage;
    [SerializeField] [HideInInspector] int moneyDrop;
    [SerializeField] [HideInInspector] float speed;
    [SerializeField] [HideInInspector] float detectionRadio;
    [SerializeField] [HideInInspector] float activateChance;
    [SerializeField] [HideInInspector] float timer;
    [SerializeField] [HideInInspector] float visionRange;
    [SerializeField] [HideInInspector] float attackRange;
    [SerializeField] [HideInInspector] bool chanceOfActivateOnDeath;
    [SerializeField] [HideInInspector] bool detectsPlayer;
    [SerializeField] [HideInInspector] bool playerDetected;
    [SerializeField] [HideInInspector] bool fly;
    [SerializeField] [HideInInspector] bool onEnemyDeath;
    [SerializeField] [HideInInspector] bool attack;
    [SerializeField] [HideInInspector] bool dropMoney;
    [SerializeField] [HideInInspector] bool startsGoingRight;
    [SerializeField] [HideInInspector] bool startsGoingUp;

    [SerializeField] [HideInInspector] Vector2 randomGroundPatrol;
    [SerializeField] [HideInInspector] Vector2 randomFlyDirMax;
    [SerializeField] [HideInInspector] Vector2 randomFlyDirMin;

    [SerializeField] Transform groundDetection;
    [SerializeField] Transform wallDetection;
    [SerializeField] [HideInInspector] RaycastHit2D groundDetector;
    [SerializeField] [HideInInspector] RaycastHit2D wallDetector;
    Vector3 rightEulerAngle = new Vector3(0, -180, 0);
    Vector3 leftEulerAngle = new Vector3(0, 0, 0);
    [HideInInspector] public CustomEvents.E_Enemy imDie = new CustomEvents.E_Enemy();
    #endregion
    void Start()
    {
        if (startsGoingRight)
        {
            transform.eulerAngles = rightEulerAngle;
        }
    }

    void Update()
    {
        if (!playerDetected) //idle
        {
            if ((fly == false && groundIdelEnemy == groundIdleMovments.movesSideways))
            {
                GroundMovingSideways();
            }
            if ((fly == true && flyIdleEnemy == flyIdleMovments.movesSideways))
            {
                FlyMovingSideways();
            }
        }
        else
        {
        }
    }

    public void Hited(int damage)
    {
        health -= damage;
        if (health <= 0)
            imDie.Invoke(this);
    }

    private void FlyMovingSideways()
    {
        wallDetector = Physics2D.Raycast(wallDetection.position, Vector2.left, 0.5f);
        if (wallDetector.collider == true)
        {
            if (wallDetector.collider.gameObject.tag == "Ground")
            {
                startsGoingRight = !startsGoingRight;
            }
        }
        if (startsGoingRight)
        {
            //   wallDetector = Physics2D.Raycast(groundDetection.position, Vector2.right, 0.5f);
            transform.position += Vector3.right * speed * Time.deltaTime;
            transform.eulerAngles = rightEulerAngle;
        }
        else
        {
            //    wallDetector = Physics2D.Raycast(groundDetection.position, Vector2.left, 0.5f);
            transform.position += (Vector3.left * speed * Time.deltaTime);
            transform.eulerAngles = leftEulerAngle;
        }
    }

    private void GroundMovingSideways()
    {
        groundDetector = Physics2D.Raycast(groundDetection.position, Vector2.down, 2f);
        wallDetector = Physics2D.Raycast(wallDetection.position, Vector2.left, 0.5f);
        if (wallDetector.collider == true)
            {
            if (wallDetector.collider.gameObject.tag == "Ground")
            {
                Flip();
                startsGoingRight = !startsGoingRight;
                
            }
        }
        if (groundDetector.collider == false)
        {
            Flip();
            startsGoingRight = !startsGoingRight;
            

        }
        if (startsGoingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position += (Vector3.left * speed * Time.deltaTime);
        }
    }

    private void Flip()
    {
        if (startsGoingRight)
        {
            transform.eulerAngles = leftEulerAngle;
        }
        else { transform.eulerAngles = rightEulerAngle; }
    }

    #region EnemyInspectorEditor
#if UNITY_EDITOR
    [CustomEditor(typeof(Enemy))]
    public class EnemyEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var enemy = (Enemy)target;
            if (enemy == null) return;
            EditorUtility.SetDirty(target);
            //EditorUtility.SetDirty(enemy);
            BaseDetails(enemy);
            MoneyDetails(enemy);
            MovmentDetails(enemy);
            BehaviorDetails(enemy);

        }

        private static void BaseDetails(Enemy enemy)
        {
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
        }

        private static void MoneyDetails(Enemy enemy)
        {
            enemy.dropMoney = EditorGUILayout.Toggle("Enemy drop money", enemy.dropMoney);
            if (enemy.dropMoney)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Money droped", GUILayout.MaxWidth(235));
                enemy.moneyDrop = EditorGUILayout.IntField(enemy.moneyDrop);
                EditorGUILayout.EndHorizontal();
            }
        }

        private static void MovmentDetails(Enemy enemy)
        {
            enemy.fly = EditorGUILayout.Toggle("Enemy flies", enemy.fly);
            if (enemy.fly)
            {
                enemy.flyIdleEnemy = (flyIdleMovments)EditorGUILayout.EnumPopup("Idle Movments", enemy.flyIdleEnemy);
                if (enemy.flyIdleEnemy == flyIdleMovments.movesSideways)
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
        }

        private static void BehaviorDetails(Enemy enemy)
        {
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
