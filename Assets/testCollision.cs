using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCollision : MonoBehaviour
{
    public Vector3 checkpoint;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("choca");
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.position = checkpoint;
        }
    }
}
