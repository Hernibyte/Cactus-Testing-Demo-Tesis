using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class h_ThornBehaviour : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] LayerMask floor_LayerMask;

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (h_Utils.LayerMaskContains(floor_LayerMask, other.gameObject.layer))
            Destroy(gameObject);
    }
}
