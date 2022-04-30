using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    public float speed;
    public bool goingRight;
    public Transform groundDetection;
    RaycastHit2D groundDetector;
    Vector3 rightEulerAngle = new Vector3(0, -180, 0);
    Vector3 leftEulerAngle = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        if (goingRight)
        {
            transform.eulerAngles = rightEulerAngle;
        }
        else
        {
            transform.eulerAngles = leftEulerAngle;
        }
    }

    // Update is called once per frame
    void Update()
    {

        groundDetector = Physics2D.Raycast(groundDetection.position, Vector2.down, 2f);
        if (groundDetector.collider == false)
        {
            goingRight = !goingRight;
        }
        if (goingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            transform.eulerAngles = rightEulerAngle;
        }
        else
        {
            transform.position += (Vector3.left * speed * Time.deltaTime);

            transform.eulerAngles = leftEulerAngle;
        }
    }
}
