using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class h_ChargedThornBehaviour : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] LayerMask floor_LayerMask;

    BoxCollider2D boxCollider2D;
    bool imMove = true;
    float timeToDie = 0;

    void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Move();
        TimeToDisappear();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (h_Utils.LayerMaskContains(floor_LayerMask, other.gameObject.layer))
        {
            imMove = false;
            gameObject.layer = 8;
            boxCollider2D.isTrigger = false;
        }
    }

    void Move()
    {
        if (imMove)
            transform.position += transform.right * speed * Time.deltaTime;
    }

    void TimeToDisappear()
    {
        if (!imMove)
        {
            timeToDie += Time.deltaTime;
            if (timeToDie >= 4)
                Destroy(gameObject);
        }
    }
}
