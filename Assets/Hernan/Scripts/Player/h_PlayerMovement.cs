﻿using System.Collections;
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
    public bool isGrounded;
    public bool isJumping;
    
    float timer;
    float auxTimer;

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
    /// Permite al jugador saltar. Usa KeyCode -> Space. Es una prueba, falta limpiar el codigo.
    /// </summary>
    void Jump()
    {
        Collider2D coll = Physics2D.OverlapBox(transform.position - new Vector3(0f, 0.12f, 0f), new Vector2(0.1f, 0.01f), 0, floorLayerMask);

        if (!coll)
            isGrounded = false;
        else
            isGrounded = true;

        if (isGrounded && !isJumping && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            auxTimer = 0;
        }

        if (!isGrounded && !isJumping)
        {
            if (auxTimer <= 2f)
                auxTimer += Time.deltaTime;

            rb2D.AddForce(Vector2.down * 2 * auxTimer, ForceMode2D.Impulse);
        }

        if (isJumping && Input.GetKey(KeyCode.Space))
        {
            timer += Time.deltaTime;
            if (timer >= 0.3f)
            {
                isJumping = false;
                timer = 0f;
            }
            rb2D.AddForce(new Vector2(0, stats.jumpForce) * curve.Evaluate(timer), ForceMode2D.Force);
        }
        else
        {
            isJumping = false;
            timer = 0;
        }
    }
}
