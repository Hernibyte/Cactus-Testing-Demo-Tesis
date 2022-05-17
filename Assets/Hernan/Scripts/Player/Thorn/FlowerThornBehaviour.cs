using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerThornBehaviour : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float lifeTime = 6;
    [SerializeField] float xImpulse = 300;
    [SerializeField] float yImpulse = 10;
    [SerializeField] LayerMask floor_LayerMask;
    [SerializeField] LayerMask player_LayerMask;
    [HideInInspector] public CustomEvents.E_FlowerThorn imDie = new CustomEvents.E_FlowerThorn();
    [HideInInspector] public h_PlayerMovement playerMovement;

    BoxCollider2D boxCollider2D;
    Rigidbody2D rb2D;
    bool imMove = true;
    float timeToDie = 0;
    bool impulsePlayer;

    enum FlowerDirection
    {
        Up,
        Down,
        Left,
        Right
    } FlowerDirection flowerDirection;

    void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        TimeToDisappear();
        if (impulsePlayer)
            ImpulseThePlayer();
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position - (transform.right * 2.2f), new Vector3(0.6f, 2, 0.5f));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (h_Utils.LayerMaskContains(floor_LayerMask, other.gameObject.layer))
        {
            impulsePlayer = true;
            imMove = false;
            gameObject.layer = 8;
            boxCollider2D.isTrigger = false;

            float x = transform.position.x - other.ClosestPoint(transform.position).x;
            float y = transform.position.y - other.ClosestPoint(transform.position).y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                transform.position = new Vector3(transform.position.x, other.ClosestPoint(transform.position).y);
                if (x < 0)
                {
                    transform.rotation = Quaternion.identity;
                    flowerDirection = FlowerDirection.Left;
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 180);
                    flowerDirection = FlowerDirection.Right;
                }
            }
            else
            {
                transform.position = new Vector3(other.ClosestPoint(transform.position).x, transform.position.y);
                if (y < 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                    flowerDirection = FlowerDirection.Down;
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, -90);
                    flowerDirection = FlowerDirection.Up;
                }
            }
        }
    }

    void ImpulseThePlayer()
    {
        float rot = 0;
        float x = 0;
        float y = 0;

        switch(flowerDirection)
        {
            case FlowerDirection.Up:
                rot = 90;
                x = 0;
                y = yImpulse;
                break;
            case FlowerDirection.Down:
                rot = 90;
                x = 0;
                x = -yImpulse;
                break;
            case FlowerDirection.Left:
                rot = 0;
                x = -xImpulse;
                y = 0;
                break;
            case FlowerDirection.Right:
                rot = 0;
                x = xImpulse;
                y = 0;
                break;
        }

        Collider2D coll = Physics2D.OverlapBox(transform.position - (transform.right * 2.2f), new Vector3(1f, 2, 0.5f), rot, player_LayerMask);
        if (coll)
        {
            playerMovement.ApplyImpulse(x, y, 0.16f);
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
