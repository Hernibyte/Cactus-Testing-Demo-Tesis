using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AnimC : MonoBehaviour
{
    [SerializeField] SpriteRenderer playerBody;
    [SerializeField] h_PlayerMovement playerMovement;

    void Awake()
    {
        playerMovement.lookLeft_Event.AddListener(LookToLeft);
        playerMovement.lookRight_Event.AddListener(LookToRight);
    }

    void LookToLeft()
    {
        playerBody.flipX = false;
    }

    void LookToRight()
    {
        playerBody.flipX = true;
    }
}
