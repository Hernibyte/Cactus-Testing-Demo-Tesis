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

    h_PlayerAttack playerAttack;
    h_PlayerStats stats;
    Rigidbody2D rb2D;
    public bool isGrounded;
    public bool isJumping;
    float timer;

    void Awake()
    {
        playerAttack = GetComponent<h_PlayerAttack>();
        stats = GetComponent<h_PlayerStats>();
        rb2D = GetComponent<Rigidbody2D>();
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
        Gizmos.DrawCube(transform.position + new Vector3(0, -5.45f, 0), new Vector2(4.8f, 0.5f));
        Gizmos.DrawCube(transform.position + new Vector3(0, 3.54f, 0), new Vector2(4.8f, 0.5f));
        Gizmos.DrawCube(transform.position + new Vector3(2.4f, -1f, 0), new Vector2(1, 8.8f));
        Gizmos.DrawCube(transform.position + new Vector3(-2.4f, -1f, 0), new Vector2(1, 8.8f));
    }

    /// <summary>
    /// Mueve al jugador en base a entradas de teclado.
    /// </summary>
    void Move()
    {
        Collider2D coll2 = Physics2D.OverlapBox(transform.position + new Vector3(2.4f, -1f, 0), new Vector2(1, 8.8f), 0, floorLayerMask);
        Collider2D coll1 = Physics2D.OverlapBox(transform.position + new Vector3(-2.4f, -1f, 0), new Vector2(1, 8.8f), 0, floorLayerMask);

        float x = Input.GetAxis("Horizontal") * stats.movementSpeed;

        if (coll1)
            if (x < 0)
                x = 0;
        if (coll2)
            if (x > 0)
                x = 0;

        if (x > 0)
        {
            playerAttack.meleeAttackDirection = h_Direction.Right;
            lookRight_Event.Invoke();
        }
        else if (x < 0)
        {
            playerAttack.meleeAttackDirection = h_Direction.Left;
            lookLeft_Event.Invoke();
        }

        rb2D.velocity = new Vector2(x, rb2D.velocity.y);
    }

    /// <summary>
    /// Permite al jugador saltar. Usa KeyCode -> Space. Es una prueba, falta limpiar el codigo.
    /// </summary>
    void Jump()
    {
        Collider2D coll = Physics2D.OverlapBox(transform.position + new Vector3(0, -5.45f, 0), new Vector2(4.8f, 0.5f), 0, floorLayerMask);
        Collider2D coll2 = Physics2D.OverlapBox(transform.position + new Vector3(0, 3.54f, 0), new Vector2(4.8f, 0.5f), 0, floorLayerMask);

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
            if (timer >= 0.6f)
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
}
