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

public enum SpecialShootType
{
    ChargedShoot,
    FlowerShoot
}

public class Ev_ChargedThorn : UnityEvent<GameObject> {}

public class h_PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject simpleThorn;
    [SerializeField] GameObject chargedThorn;
    [SerializeField] GameObject flowerThorn;
    [SerializeField] LayerMask enemy_LayerMask;
    //public h_Direction meleeAttackDirection;
    [SerializeField] float timeToNormalShoot;
    [SerializeField] float timeToChargedShoot;
    [SerializeField] float timeToFlowerShoot;
    [HideInInspector] public Ev_ChargedThorn shootChargedThorn = new Ev_ChargedThorn();
    [HideInInspector] public CustomEvents.E_FlowerThorn shootFlowerThorn = new CustomEvents.E_FlowerThorn();
    [SerializeField] Animator PlayerAnimator;
    float delayNormalRangeAttack = 0;
    bool normalRangeAttackAvailable = false;
    float delayChargedRangeAttack = 0;
    bool chagedRangeAttackAvailable = false;
    float delayFlowerRangeAttack = 0;
    public bool flowerRangeAttackAvailable = false;
    Transform mouseWorldPosition;
    //Vector2 meleeAttackPosition = new Vector2();
    h_PlayerStats stats;
    SpecialShootType shootType = SpecialShootType.ChargedShoot;

    public bool flowerPowerUpObtained;

    void Awake()
    {
        stats = GetComponent<h_PlayerStats>();

        mouseWorldPosition = FindObjectOfType<h_MouseTracker>().transform;
    }

    void Update()
    {
        //MeleeAttack();
        RangeAttack();

        //if (Input.GetKeyDown(KeyCode.LeftShift) && flowerPowerUpObtained)
        //{
        //    switch(shootType)
        //    {
        //        case SpecialShootType.ChargedShoot:
        //            shootType = SpecialShootType.FlowerShoot;
        //            break;
        //        case SpecialShootType.FlowerShoot:
        //            shootType = SpecialShootType.ChargedShoot;
        //            break;
        //    }
        //}
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
        //PlayerAnimator.SetTrigger("Shoot");
        if (delayNormalRangeAttack <= timeToNormalShoot)
            delayNormalRangeAttack += Time.deltaTime;
        else
            normalRangeAttackAvailable = true;

        if (delayChargedRangeAttack <= timeToChargedShoot)
            delayChargedRangeAttack += Time.deltaTime;
        else
            chagedRangeAttackAvailable = true;

        if (delayFlowerRangeAttack <= timeToFlowerShoot)
            delayFlowerRangeAttack += Time.deltaTime;
        else
            flowerRangeAttackAvailable = true;

        if (chagedRangeAttackAvailable && Input.GetKey(KeyCode.Mouse1))
        {
            shootChargedThorn.Invoke(GenerateThorn(chargedThorn));
            chagedRangeAttackAvailable = false;
            delayChargedRangeAttack = 0f;
        }

        if (flowerPowerUpObtained)
            if (flowerRangeAttackAvailable && Input.GetKey(KeyCode.F))
            {
                shootFlowerThorn.Invoke(GenerateThorn(flowerThorn, -90));
                flowerRangeAttackAvailable = false;
                delayFlowerRangeAttack = 0f;
            }

        if (normalRangeAttackAvailable && Input.GetKey(KeyCode.Mouse0))
        {
            //GenerateThorn(simpleThorn); TODO Sacar esto en caso de usar los ataques normales
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

    GameObject GenerateThorn(GameObject thornType, float angle)
    {
        GameObject obj = Instantiate(thornType, transform.position, Quaternion.identity);
        obj.transform.rotation = Quaternion.Euler(0, 0, angle);
        return obj;
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

    public bool getChargedRangeAttackAvailable()
    {
        return chagedRangeAttackAvailable;
    }
    public bool getFlowerAvailable()
    {
        return flowerRangeAttackAvailable;
    }
}
