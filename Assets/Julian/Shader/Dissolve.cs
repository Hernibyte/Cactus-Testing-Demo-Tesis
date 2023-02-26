using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    public Material material;

    public bool isDissolving;
    public float fade = 1f;

    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
        isDissolving = false;
    }

    void Update()
    {        
        if (isDissolving)
        {
            fade -= Time.deltaTime;
            if (fade <= 0f)
            {
                fade = 0f;
                isDissolving = false;
            }
            this.GetComponent<SpriteRenderer>().material.SetFloat("_Fade", fade);
        }
        if (!isDissolving)
        {
            if(fade <= 1)
            {
                fade += Time.deltaTime ;
                this.GetComponent<SpriteRenderer>().material.SetFloat("_Fade", fade);
            }
        }
    }
}
