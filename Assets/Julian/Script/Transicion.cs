using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transicion : MonoBehaviour
{
    public string nextLevel;
    public float timeToChange;
    //

    void Start()
    {
        StartCoroutine(ChangeScenes());
    }

    IEnumerator ChangeScenes()
    {
        yield return new WaitForSeconds(timeToChange);
        SceneManager.LoadScene(nextLevel);
    }
}
