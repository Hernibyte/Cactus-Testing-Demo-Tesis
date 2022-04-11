using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class h_MouseTracker : MonoBehaviour
{
    Camera mainCam;

    void Awake()
    {
        mainCam = Camera.main;
    }

    void FixedUpdate()
    {
        Vector3 m_P = Input.mousePosition;
        Vector3 pointWorld = mainCam.ScreenToWorldPoint(Input.mousePosition);
        pointWorld.z = 0;
        transform.position = pointWorld;
    }
}
