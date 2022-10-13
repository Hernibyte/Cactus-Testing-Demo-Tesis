using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alternator : MonoBehaviour
{
    public bool inAlternator;
    public GameObject Past;
    public GameObject Future;
    public h_GameManager GM;

    private void Start()
    {
        inAlternator = false;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("log3");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("log4");
            inAlternator = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        inAlternator = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("log1");
            if (inAlternator)
            {
                Debug.Log("log2");
                if (GM.isInPast == true)
                {
                    Future.SetActive(true);
                    Past.SetActive(false);
                    GM.isInPast = false;
                }
                else
                {
                    Future.SetActive(false);
                    Past.SetActive(true);
                    GM.isInPast = true;
                }
            }
        }
    }
}
