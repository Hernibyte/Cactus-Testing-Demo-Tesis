using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Define el movimiento del jugador
/// </summary>
public class h_PlayerMovement : MonoBehaviour
{
    [SerializeField] LayerMask floorLayerMask;
    [SerializeField] AnimationCurve curve;
    [HideInInspector] public UnityEvent lookLeft_Event = new UnityEvent();
    [HideInInspector] public UnityEvent lookRight_Event = new UnityEvent();

    Vector2[] checkersPositions = new Vector2[4];
    Vector2[] checkersSize = new Vector2[4];

    //h_PlayerAttack playerAttack;
    h_PlayerStats stats;
    Rigidbody2D rb2D;
    BoxCollider2D boxCollider2D;
    bool isGrounded;
    bool isJumping;
    float timer;
    float slowdownValue;

    void Awake()
    {
        //playerAttack = GetComponent<h_PlayerAttack>();
        stats = GetComponent<h_PlayerStats>();
        rb2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        SetCheckersValue();
    }

    void Update()
    {
        Jump();
    }

    void FixedUpdate()
    {
        Move();
    }

    void OnDrawGizmos()
    {
        //Gizmos.DrawCube(transform.position + new Vector3(0, -5.45f, 0), new Vector2(4.8f, 0.5f));
        //Gizmos.DrawCube(transform.position + new Vector3(0, 3.54f, 0), new Vector2(4.8f, 0.5f));
        //Gizmos.DrawCube(transform.position + new Vector3(2.4f, -1f, 0), new Vector2(1, 8.8f));
        //Gizmos.DrawCube(transform.position + new Vector3(-2.4f, -1f, 0), new Vector2(1, 8.8f));
        Gizmos.DrawCube(transform.position + new Vector3(checkersPositions[0].x, checkersPositions[0].y), checkersSize[0]);
        Gizmos.DrawCube(transform.position + new Vector3(checkersPositions[1].x, checkersPositions[1].y), checkersSize[1]);
        Gizmos.DrawCube(transform.position + new Vector3(checkersPositions[2].x, checkersPositions[2].y), checkersSize[2]);
        Gizmos.DrawCube(transform.position + new Vector3(checkersPositions[3].x, checkersPositions[3].y), checkersSize[3]);
    }

    /// <summary>
    /// Mueve al jugador en base a entradas de teclado.
    /// </summary>
    void Move()
    {
        Collider2D coll2 = Physics2D.OverlapBox(transform.position + new Vector3(checkersPositions[3].x, checkersPositions[3].y), checkersSize[3], 0, floorLayerMask);
        Collider2D coll1 = Physics2D.OverlapBox(transform.position + new Vector3(checkersPositions[2].x, checkersPositions[2].y), checkersSize[2], 0, floorLayerMask);

        float x = Input.GetAxis("Horizontal") * stats.movementSpeed;

        if (coll1)
            if (x < 0)
                x = 0;
        if (coll2)
            if (x > 0)
                x = 0;

        if (x > 0)
        {
            //playerAttack.meleeAttackDirection = h_Direction.Right;
            lookRight_Event.Invoke();
        }
        else if (x < 0)
        {
            //playerAttack.meleeAttackDirection = h_Direction.Left;
            lookLeft_Event.Invoke();
        }

        rb2D.velocity = new Vector2(x + slowdownValue, rb2D.velocity.y);
    }

    /// <summary>
    /// Permite al jugador saltar. Usa KeyCode -> Space. Es una prueba, falta limpiar el codigo.
    /// </summary>
    void Jump()
    {
        Collider2D coll = Physics2D.OverlapBox(transform.position + new Vector3(checkersPositions[0].x, checkersPositions[0].y), checkersSize[0], 0, floorLayerMask);
        Collider2D coll2 = Physics2D.OverlapBox(transform.position + new Vector3(checkersPositions[1].x, checkersPositions[1].y), checkersSize[1], 0, floorLayerMask);

        if (coll)
            isGrounded = true;
        else
            isGrounded = false;

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            timer = 0;
        }

        if (isJumping && Input.GetKey(KeyCode.Space))
        {
            rb2D.velocity += new Vector2(0, curve.Evaluate(timer) * stats.jumpForce * Time.deltaTime);
            timer += Time.deltaTime;
            if (timer >= 0.4f)
            {
                rb2D.velocity = new Vector2(0, 0);
                isJumping = false;
                timer = 0;
            }
        }
        else if (isJumping && !Input.GetKey(KeyCode.Space))
        {
            rb2D.velocity = new Vector2(0, 0);
            isJumping = false;
            timer = 0;
        }

        if (coll2)
            isJumping = false;
    }

    void SetCheckersValue()
    {
        checkersPositions[0] = new Vector2(
            transform.position.x, 
            transform.position.y + boxCollider2D.offset.y - (boxCollider2D.size.y / 2)
        );
        checkersPositions[1] = new Vector2(
            transform.position.x,
            transform.position.y + boxCollider2D.offset.y + (boxCollider2D.size.y / 2)
        );
        checkersPositions[2] = new Vector2(
            transform.position.x + boxCollider2D.offset.x - (boxCollider2D.size.x / 2),
            transform.position.y + boxCollider2D.offset.y
        );
        checkersPositions[3] = new Vector2(
            transform.position.x + boxCollider2D.offset.x + (boxCollider2D.size.x / 2),
            transform.position.y + boxCollider2D.offset.y
        );

        checkersSize[0] = new Vector2(
            boxCollider2D.size.x - 0.2f,
            0.5f
        );
        checkersSize[1] = new Vector2(
            boxCollider2D.size.x - 0.2f,
            0.5f
        );
        checkersSize[2] = new Vector2(
            1,
            boxCollider2D.size.y - 0.2f
        );
        checkersSize[3] = new Vector2(
            1,
            boxCollider2D.size.y - 0.2f
        );
    }

    public void ApplySlowdown(float value)
    {
        slowdownValue = value;
    }

    public void DeleteSlowdown()
    {
        slowdownValue = 0;
    }
}
