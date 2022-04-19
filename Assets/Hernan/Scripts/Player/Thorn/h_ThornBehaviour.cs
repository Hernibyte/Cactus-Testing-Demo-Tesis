using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class h_ThornBehaviour : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] LayerMask floor_LayerMask;
    [SerializeField] LayerMask enemy_LayerMask;

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
        if (h_Utils.LayerMaskContains(enemy_LayerMask, other.gameObject.layer))
        {
            IHitable hitable = other.GetComponent<IHitable>();
            hitable.Hited(0);
            Destroy(gameObject);
        }
    }
}
