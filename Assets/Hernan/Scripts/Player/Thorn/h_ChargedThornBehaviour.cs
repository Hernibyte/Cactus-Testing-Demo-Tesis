using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class h_ChargedThornBehaviour : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float lifeTime = 4;
    [SerializeField] LayerMask floor_LayerMask;
    [SerializeField] LayerMask hazard_LayerMask;
    [HideInInspector] public Ev_ChargedThorn imDie = new Ev_ChargedThorn();

    BoxCollider2D boxCollider2D;
    Rigidbody2D rb2D;
    bool imMove = true;
    float timeToDie = 0;

    void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
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

            float x = transform.position.x - other.ClosestPoint(transform.position).x;
            float y = transform.position.y - other.ClosestPoint(transform.position).y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                transform.position = new Vector3(transform.position.x, other.ClosestPoint(transform.position).y);
                if (x < 0)
                    transform.rotation = Quaternion.identity;
                else
                    transform.rotation = Quaternion.Euler(0, 0, 180);

            }
            else
            {
                transform.position = new Vector3(other.ClosestPoint(transform.position).x, transform.position.y);
                if (y < 0)
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                else
                    transform.rotation = Quaternion.Euler(0, 0, -90);
            }
        }
        if (other.gameObject.tag == "Hazards")
        {
            Debug.Log("chocó contra hazard");
            imDie.Invoke(this.gameObject);
            Destroy(this.gameObject);
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
            if (timeToDie >= lifeTime)
            {
                imDie.Invoke(this.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
