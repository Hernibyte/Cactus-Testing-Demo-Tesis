using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class h_PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject simpleThorn;
    [SerializeField] GameObject chargedThorn;
    [SerializeField] Transform mouseWorldPosition;

    float delayRangeAttack = 0;
    float chargeRangeAttack = 0;

    void Update()
    {
        MeleeAttack();
        RangeAttack();
    }

    void MeleeAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

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
