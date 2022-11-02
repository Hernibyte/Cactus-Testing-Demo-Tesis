using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    #region Public

    public void Activate()
    {
        _isActivated = true;
        gameObject.SetActive(true);
        yaSeActivoPa = true;
    }

    [HideInInspector] public bool yaSeActivoPa;
    
    #endregion
    #region Private

    private void Update()
    {
        if (!_isActivated) return;

        _timer += Time.deltaTime;
        if (_timer >= 3f)
        {
            _timer = 0f;
            _isActivated = false;
            gameObject.SetActive(false);
        }
    }

    private float _timer;
    private bool _isActivated = false;

    #endregion
}
