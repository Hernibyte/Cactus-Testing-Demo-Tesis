using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomController 
{
    public class PlayerAnimation : MonoBehaviour 
    {
        [SerializeField] SpriteRenderer playerBody;
        h_PlayerMovement playerMovement;

        void Awake()
        {
            playerMovement = GetComponent<h_PlayerMovement>();

            playerMovement.lookLeft_Event.AddListener(LookToLeft);
            playerMovement.lookRight_Event.AddListener(LookToRight);
        }

        void LookToLeft()
        {
            playerBody.flipX = true;
        }

        void LookToRight()
        {
            playerBody.flipX = false;
        }
    }
}