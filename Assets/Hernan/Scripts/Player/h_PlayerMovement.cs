using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Define el movimiento del jugador
/// </summary>
public class h_PlayerMovement : MonoBehaviour
{
    [SerializeField] LayerMask floorLayerMask;
    [SerializeField] AnimationCurve curve;
    h_PlayerStats stats;
    Rigidbody2D rb2D;
    bool isGrounded;
    bool isJumping;
    float timer;

    void Awake()
    {
        stats = GetComponent<h_PlayerStats>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    /// <summary>
    /// Mueve al jugador en base a entradas de teclado.
    /// </summary>
    void Move()
    {
        Collider2D coll1 = Physics2D.OverlapBox(transform.position - new Vector3(0.06f, 0f, 0f), new Vector2(0.01f, 0.18f), 0, floorLayerMask);
        Collider2D coll2 = Physics2D.OverlapBox(transform.position - new Vector3(-0.06f, 0f, 0f), new Vector2(0.01f, 0.18f), 0, floorLayerMask);

        float x = Input.GetAxis("Horizontal") * stats.movementSpeed * Time.deltaTime;

        if (coll1)
            if (x < 0)
                x = 0;
        if (coll2)
            if (x > 0)
                x = 0;

        Vector3 newPos = new Vector3(x, 0, 0);
        rb2D.AddForce(newPos, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Permite al jugador saltar. Usa KeyCode -> Space.
    /// </summary>
    void Jump()
    {
        Collider2D coll = Physics2D.OverlapBox(transform.position - new Vector3(0f, 0.12f, 0f), new Vector2(0.1f, 0.01f), 0, floorLayerMask);

        if (isGrounded && !coll)
            isGrounded = false;
        else if (!isGrounded && coll)
        {
            isGrounded = true;
            timer = 0;
        }

        if (isJumping)
        {
            timer += Time.deltaTime;
            if (timer <= 0.2f)
                rb2D.AddForce(new Vector2(0, stats.jumpForce) * curve.Evaluate(timer), ForceMode2D.Impulse);
            else
            {
                isJumping = false;
            }
        }
        
        if (!isGrounded && !isJumping)
        {
            if (timer <= 1) 
                timer += Time.deltaTime;
            rb2D.AddForce(Vector2.down * 60 * timer, ForceMode2D.Force);
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            isJumping = true;
    }
}
