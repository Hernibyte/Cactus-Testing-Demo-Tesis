using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Ambient
{
    public class WindAreaBehaviour : MonoBehaviour
    {
        [SerializeField] float windForce;
        [SerializeField] LayerMask player_LayerMask;
        [HideInInspector] public CustomEvents.E_WindStart playerEnter = new CustomEvents.E_WindStart();
        [HideInInspector] public UnityEvent playerExit = new UnityEvent();

        void OnTriggerEnter2D(Collider2D other)
        {
            if (h_Utils.LayerMaskContains(player_LayerMask, other.gameObject.layer))
                playerEnter.Invoke(windForce);
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (h_Utils.LayerMaskContains(player_LayerMask, other.gameObject.layer))
                playerExit.Invoke();
        }
    }
}
