using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaceholderForBuild : MonoBehaviour
{
    public Vector3 checkpoint;
    public h_PlayerAttack playerAttack;
    public h_GameManager gm;
    [SerializeField] Animator PlayerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        { 
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Game_for_Proto");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PowerUp" /*&& Input.GetKeyDown(KeyCode.E)*/)
        {
            PlayerAnimator.SetTrigger("PickUp");
            if (collision.gameObject.name == "Trampolin")
            {
                playerAttack.flowerPowerUpObtained = true;
            }
            if (collision.gameObject.name == "PinchoExtra")
            {
                gm.addChargedShootThornMaxAmount();
            }
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Hazards")
        {
           // this.gameObject.transform.position = checkpoint;
        }

        
    }
}
