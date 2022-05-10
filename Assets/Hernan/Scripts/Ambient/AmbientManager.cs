using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ambient
{
    public class AmbientManager : MonoBehaviour
    {
        [SerializeField] h_PlayerMovement playerMovement;
        WindAreaBehaviour[] windAreas;

        void Awake()
        {
            windAreas = FindObjectsOfType<WindAreaBehaviour>();
        }

        void Start()
        {
            foreach (WindAreaBehaviour windArea in windAreas)
            {
                windArea.playerEnter.AddListener(playerMovement.ApplySlowdown);
                windArea.playerExit.AddListener(playerMovement.DeleteSlowdown);
            }
        }
    }
}
