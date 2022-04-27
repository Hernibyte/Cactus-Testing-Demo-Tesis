using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum h_Direction
{
    Right,
    Left,
    Up,
    Down
}

public class h_PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject simpleThorn;
    [SerializeField] GameObject chargedThorn;
    [SerializeField] Transform mouseWorldPosition;
    [SerializeField] LayerMask enemy_LayerMask;
    public h_Direction meleeAttackDirection;

    float delayRangeAttack = 0;
    float chargeRangeAttack = 0;
    Vector2 meleeAttackPosition = new Vector2();

    void Update()
    {
        MeleeAttack();
        RangeAttack();
    }

    void OnDrawGizmos()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Gizmos.DrawCube(meleeAttackPosition, new Vector3(9, 4.5f, 1));
        }
    }

    void MeleeAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (meleeAttackDirection)
            {
                case h_Direction.Right:
                    meleeAttackPosition = transform.position + (Vector3.right * 6.5f);
                    break;
                case h_Direction.Left:
                    meleeAttackPosition = transform.position + (Vector3.left * 6.5f);
                    break;
            }

            Collider2D coll = Physics2D.OverlapBox(meleeAttackPosition, new Vector3(9, 4.5f, 1), 0, enemy_LayerMask);
            if (coll)
            {
                IHitable hitable = coll.GetComponent<IHitable>();
                hitable.Hited(0);
            }
        }
    }

    void RangeAttack()
    {
        if (delayRangeAttack <= 0.55f)
            delayRangeAttack += Time.deltaTime;

        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (chargeRangeAttack <= 0.8f)
                chargeRangeAttack += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            if (delayRangeAttack >= 0.5f)
            {
                if (chargeRangeAttack < 0.25f)
                {
                    GenerateThorn(simpleThorn);
                }
                else
                {
                    GenerateThorn(chargedThorn);
                }

                delayRangeAttack = 0;
                chargeRangeAttack = 0;
            }
        }
    }

    void GenerateThorn(GameObject thornType)
    {
        Vector3 dir = mouseWorldPosition.position - transform.position;
        float angle = 0;

        if (dir.y < 0)
            angle = Vector2.Angle(Vector2.left, dir) + 180;
        else
            angle = Vector2.Angle(Vector2.right, dir);

        GameObject obj = Instantiate(thornType, transform.position, Quaternion.identity);
        obj.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
