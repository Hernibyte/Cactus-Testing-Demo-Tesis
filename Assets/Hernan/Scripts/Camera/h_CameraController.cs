using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class h_CameraController : MonoBehaviour
{
    [SerializeField] Transform player;

    void Awake()
    {
        player = FindObjectOfType<h_PlayerMovement>().transform;
    }

    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, -5);
    }
}
