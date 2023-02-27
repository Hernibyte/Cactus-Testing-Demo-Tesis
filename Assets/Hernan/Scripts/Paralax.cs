using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    #region Private

    private void Update()
    {
        if (playerMovement.x != 0)
            transform.position -= new Vector3(playerMovement.x * diff * Time.deltaTime, 0);

        if (transform.position.x > maxPos)
            transform.position = new Vector3(maxPos, transform.position.y, transform.position.z);
        else if (transform.position.x < minPos)
            transform.position = new Vector3(minPos, transform.position.y, transform.position.z);
    }

    [SerializeField] private h_PlayerMovement playerMovement;
    [SerializeField] private float diff;
    [SerializeField] private float maxPos;
    [SerializeField] private float minPos;

    #endregion
}
