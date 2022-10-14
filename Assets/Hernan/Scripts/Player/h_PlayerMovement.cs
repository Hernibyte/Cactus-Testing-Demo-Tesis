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
    //[SerializeField] AnimationCurve curve;
    //[SerializeField] float coyoteTime;
    [SerializeField] Animator PlayerAnimator;
    [HideInInspector] public UnityEvent lookLeft_Event = new UnityEvent();
    [HideInInspector] public UnityEvent lookRight_Event = new UnityEvent();

    //h_PlayerAttack playerAttack;
    h_PlayerStats stats;
    public Rigidbody2D rb2D { get; set; }
    BoxCollider2D boxCollider2D;
    bool isGrounded;
    bool isJumping;
    float timer;
    float slowdownValue;
    //float coyoteTimer;
    bool impulseActivated;
    Vector2 movementImpulse;
    float timeToImpulse;

    void Awake()
    {
        //playerAttack = GetComponent<h_PlayerAttack>();
        stats = GetComponent<h_PlayerStats>();
        rb2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        CalculateImpulse();
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
        //Gizmos.DrawCube(transform.position + new Vector3(checkersPositions[0].x, checkersPositions[0].y), checkersSize[0]);
        //Gizmos.DrawCube(transform.position + new Vector3(checkersPositions[1].x, checkersPositions[1].y), checkersSize[1]);
        //Gizmos.DrawCube(transform.position + new Vector3(checkersPositions[2].x, checkersPositions[2].y), checkersSize[2]);
        //Gizmos.DrawCube(transform.position + new Vector3(checkersPositions[3].x, checkersPositions[3].y), checkersSize[3]);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (h_Utils.LayerMaskContains(floorLayerMask, collision.gameObject.layer))
            {
                if (collision.GetContact(0).normal == Vector2.up) isGrounded = true;
                //foreach (ContactPoint2D contact in collision.contacts)
                //{
                //    if (contact.normal == Vector2.up) isGrounded = true;
                //}
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (h_Utils.LayerMaskContains(floorLayerMask, collision.gameObject.layer))
            {
                isGrounded = false;
            }
        }
    }

    /// <summary>
    /// Mueve al jugador en base a entradas de teclado.
    /// </summary>
    void Move()
    {
        //Collider2D coll2 = Physics2D.OverlapBox(transform.position + new Vector3(checkersPositions[3].x, checkersPositions[3].y), checkersSize[3], 0, floorLayerMask);
        //Collider2D coll1 = Physics2D.OverlapBox(transform.position + new Vector3(checkersPositions[2].x, checkersPositions[2].y), checkersSize[2], 0, floorLayerMask);

        float x = Input.GetAxis("Horizontal") * stats.movementSpeed;

        //if (coll1)
        //    if (x < 0)
        //        x = 0;
        //if (coll2)
        //    if (x > 0)
        //        x = 0;

        if (x > 0)
        {
            PlayerAnimator.SetBool("Walking", true);
            //playerAttack.meleeAttackDirection = h_Direction.Right;
            lookRight_Event.Invoke();
        }
        else if (x < 0)
        {
            PlayerAnimator.SetBool("Walking", true);
            //playerAttack.meleeAttackDirection = h_Direction.Left;
            lookLeft_Event.Invoke();
        }
        else 
        {
            PlayerAnimator.SetBool("Walking", false);
        }

        if (!impulseActivated)
            rb2D.velocity = new Vector2(
                x + slowdownValue, 
                rb2D.velocity.y
            );
        else
            rb2D.velocity = new Vector2(
                movementImpulse.x,
                rb2D.velocity.y + movementImpulse.y
            );
    }

    /// <summary>
    /// Permite al jugador saltar. Usa KeyCode -> Space. Es una prueba, falta limpiar el codigo.
    /// </summary>
    void Jump()
    {
        //Collider2D coll = Physics2D.OverlapBox(transform.position + new Vector3(checkersPositions[0].x, checkersPositions[0].y), checkersSize[0], 0, floorLayerMask);
        //Collider2D coll2 = Physics2D.OverlapBox(transform.position + new Vector3(checkersPositions[1].x, checkersPositions[1].y), checkersSize[1], 0, floorLayerMask);

        //if (!coll)
        //    isGrounded = false;
        //if (coll)
        //{
        //    isGrounded = true;
        //    PlayerAnimator.SetBool("OnAir", false);
        //}
        //else
        //if (isGrounded)
        //{
        //    coyoteTimer += Time.deltaTime;
        //    if (coyoteTimer >= coyoteTime)
        //    {
        //        coyoteTimer = 0;
        //        isGrounded = false;
        //    }
        //}

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            PlayerAnimator.SetTrigger("StartJump");
            //isGrounded = false;
            //isJumping = true;
            timer = 0;
            //coyoteTimer = 0;

            rb2D.AddForce(new Vector2(0, stats.jumpForce));
        }

        //if (isJumping && Input.GetKey(KeyCode.Space))
        //{
        //    PlayerAnimator.SetBool("OnAir", true);
        //    rb2D.velocity += new Vector2(0, curve.Evaluate(timer) * stats.jumpForce * Time.deltaTime);
        //    timer += Time.deltaTime;
        //    if (timer >= 0.4f)
        //    {
        //        rb2D.velocity = new Vector2(0, 0);
        //        isJumping = false;
        //        timer = 0;
        //    }
        //}
        //else if (isJumping && !Input.GetKey(KeyCode.Space))
        //{
        //    rb2D.velocity = new Vector2(0, 0);
        //    isJumping = false;
        //    timer = 0;
        //}

        //if (coll2)
        //{
        //    PlayerAnimator.SetBool("OnAir", false);
        //    PlayerAnimator.SetTrigger("Landing");
        //    isJumping = false;
        //}
    }

    void CalculateImpulse()
    {
        if (impulseActivated)
        {
            timeToImpulse -= Time.deltaTime;
            if (timeToImpulse <= 0)
            {
                movementImpulse = new Vector2(0, 0);
                impulseActivated = false;
            }
        }
    }

    public void ApplySlowdown(float value)
    {
        slowdownValue = value;
    }

    public void ApplyImpulse(float x, float y, float time)
    {
        impulseActivated = true;
        movementImpulse.x = x;
        movementImpulse.y = y;
        timeToImpulse = time;
    }

    public void DeleteSlowdown()
    {
        slowdownValue = 0;
    }
}
