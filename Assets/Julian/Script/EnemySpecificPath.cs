using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpecificPath : MonoBehaviour
{
    public List<Transform> positions;
    int actualPos = 0;
     float speed;

    float t = 0;
    public float timeToReachTarget;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime / timeToReachTarget;
        transform.position = Vector3.LerpUnclamped(transform.position,positions[actualPos].position, Time.deltaTime * t);
        if (transform.position.x <= positions[actualPos].position.x+0.05f && 
            transform.position.x >= positions[actualPos].position.x - 0.05f && 
            transform.position.y <= positions[actualPos].position.y + 0.05f && 
            transform.position.y >= positions[actualPos].position.y - 0.05f)
        {
            actualPos++;
            if (actualPos >= positions.Count) actualPos = 0;
            t = 0;
        }
    }
}
