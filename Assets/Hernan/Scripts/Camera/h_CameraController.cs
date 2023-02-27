using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class h_CameraController : MonoBehaviour
{
    [SerializeField] Transform player;
    public bool onZoom;
    public float standardCameraSize;
    public float zoomCameraSize;
    public float offsetX;

    void Awake()
    {
        player = FindObjectOfType<h_PlayerMovement>().transform;
    }

    void Update()
    {
        if (!onZoom)
        {
            this.GetComponent<Camera>().orthographicSize = standardCameraSize;
            transform.position = new Vector3(player.position.x, player.position.y, -5);
        }
        else
        {
            this.GetComponent<Camera>().orthographicSize = zoomCameraSize;
            transform.position = new Vector3(player.position.x + offsetX  , player.position.y, -5);
        }
    }

    public void changeZoom()
    {
        onZoom = !onZoom;
    }
}
