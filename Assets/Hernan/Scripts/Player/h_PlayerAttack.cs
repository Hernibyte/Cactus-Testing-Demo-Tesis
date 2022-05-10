using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum h_Direction
{
    Right,
    Left,
    Up,
    Down
}

public class Ev_ChargedThorn : UnityEvent<GameObject> {}

public class h_PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject simpleThorn;
    [SerializeField] GameObject chargedThorn;
    [SerializeField] Transform mouseWorldPosition;
    [SerializeField] LayerMask enemy_LayerMask;
    //public h_Direction meleeAttackDirection;
    [HideInInspector] public Ev_ChargedThorn shootChargedThorn = new Ev_ChargedThorn();

    float delayNormalRangeAttack = 0;
    bool normalRangeAttackAvailable = false;
    float delayChargedRangeAttack = 0;
    bool chagedRangeAttackAvailable = false;
    //Vector2 meleeAttackPosition = new Vector2();
    h_PlayerStats stats;

    void Awake()
    {
        stats = GetComponent<h_PlayerStats>();
    }

    void Update()
    {
        //MeleeAttack();
        RangeAttack();
    }

    void OnDrawGizmos()
    {
        //if (Input.GetKey(KeyCode.Mouse0))
        //{
        //    Gizmos.DrawCube(meleeAttackPosition, new Vector3(9, 4.5f, 1));
        //}
    }

    //void MeleeAttack()
    //{
    //    if (Input.GetKeyDown(KeyCode.Mouse0))
    //    {
    //        switch (meleeAttackDirection)
    //        {
    //            case h_Direction.Right:
    //                meleeAttackPosition = transform.position + (Vector3.right * 6.5f);
    //                break;
    //            case h_Direction.Left:
    //                meleeAttackPosition = transform.position + (Vector3.left * 6.5f);
    //                break;
    //        }
//
    //        Collider2D coll = Physics2D.OverlapBox(meleeAttackPosition, new Vector3(9, 4.5f, 1), 0, enemy_LayerMask);
    //        if (coll)
    //        {
    //            IHitable hitable = coll.GetComponent<IHitable>();
    //            hitable.Hited(stats.damage);
    //        }
    //    }
    //}

    void RangeAttack()
    {
        if (delayNormalRangeAttack <= 0.55f)
            delayNormalRangeAttack += Time.deltaTime;
        else
            normalRangeAttackAvailable = true;

        if (delayChargedRangeAttack <= 1f)
            delayChargedRangeAttack += Time.deltaTime;
        else
            chagedRangeAttackAvailable = true;

        if (chagedRangeAttackAvailable && Input.GetKey(KeyCode.Mouse1))
        {
            shootChargedThorn.Invoke(GenerateThorn(chargedThorn));
            chagedRangeAttackAvailable = false;
            delayChargedRangeAttack = 0f;
        }

        if (normalRangeAttackAvailable && Input.GetKey(KeyCode.Mouse0))
        {
            GenerateThorn(simpleThorn);
            normalRangeAttackAvailable = false;
            delayNormalRangeAttack = 0;
        }

        //if (Input.GetKeyUp(KeyCode.Mouse1))
        //{
        //    if (delayRangeAttack >= 0.5f)
        //    {
        //        if (chargeRangeAttack < 0.25f)
        //        {
        //            GenerateThorn(simpleThorn);
        //        }
        //        else
        //        {
        //            shootChargedThorn.Invoke(GenerateThorn(chargedThorn));
        //        }
//
        //        delayRangeAttack = 0;
        //        chargeRangeAttack = 0;
        //    }
        //}
    }

    GameObject GenerateThorn(GameObject thornType)
    {
        Vector3 dir = mouseWorldPosition.position - transform.position;
        float angle = 0;

        if (dir.y < 0)
            angle = Vector2.Angle(Vector2.left, dir) + 180;
        else
            angle = Vector2.Angle(Vector2.right, dir);

        GameObject obj = Instantiate(thornType, transform.position, Quaternion.identity);
        obj.transform.rotation = Quaternion.Euler(0, 0, angle);
        return obj;
    }
}
