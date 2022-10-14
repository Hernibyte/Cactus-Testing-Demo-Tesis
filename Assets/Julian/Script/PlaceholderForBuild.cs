using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaceholderForBuild : MonoBehaviour
{
    public Vector3 checkpoint;
    public h_PlayerAttack playerAttack;
    public h_GameManager gm;
    public LevelManager LvlM;
    public List<Sprite> colectables;
    [SerializeField] Animator PlayerAnimator;
    // Start is called before the first frame update
    void Awake()
    {
        gm = FindObjectOfType<h_GameManager>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CheckpointOff")
        {
            checkpoint = collision.transform.position;
        }

        if (collision.gameObject.tag == "Collectable")
        {
            if(collision.gameObject.name == "SombreroDeCopa")
            {
                LvlM.collectedColectables.Add(colectables[0]);
                LvlM.notCollectedColectables.Remove(colectables[0]);
            }
            if (collision.gameObject.name == "Agua")
            {
                LvlM.collectedColectables.Add(colectables[1]);
                LvlM.notCollectedColectables.Remove(colectables[1]);
            }
            if (collision.gameObject.name == "CajaFuturista")
            {
                LvlM.collectedColectables.Add(colectables[2]);
                LvlM.notCollectedColectables.Remove(colectables[2]);
            }
            if (collision.gameObject.name == "Gas")
            {
                LvlM.collectedColectables.Add(colectables[3]);
                LvlM.notCollectedColectables.Remove(colectables[3]);
            }
            if (collision.gameObject.name == "Mapa")
            {
                LvlM.collectedColectables.Add(colectables[4]);
                LvlM.notCollectedColectables.Remove(colectables[4]);
            }

            Destroy(collision.gameObject);

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bad")
        {
           this.gameObject.transform.position = checkpoint;
        }  


    }
}
