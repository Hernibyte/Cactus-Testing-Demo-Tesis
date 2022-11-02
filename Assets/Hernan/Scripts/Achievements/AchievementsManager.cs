using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsManager : MonoBehaviour
{
    #region Public
    
    public void AddJump() => _jumpCount++;

    public void SetTimeofLvL1(float time) => _timeOfLvL1 = time;
    
    public void SetTimeofLvL2(float time) => _timeOfLvL2 = time;
    
    public void SetTimeofLvL3(float time) => _timeOfLvL3 = time;

    public void SetPlayerDeath(h_PlayerInteraction pj) => _playerIsDead = true;

    public void PassLvL()
    {   
        if (_playerIsDead)
        {
            _passLvL = true;
        }
    }

    public void Si()
    {
        _playerIsDead = false;
        _player = FindObjectOfType<h_PlayerInteraction>();
        if (_player)
            _player.imDie?.AddListener(SetPlayerDeath);

        _passLvL = false;
        _endLevel = FindObjectOfType<EndLevel>();
        if (_endLevel)
            _endLevel.endLvLCallback?.AddListener(PassLvL);

        _playerMovement = FindObjectOfType<h_PlayerMovement>();
        if (_playerMovement)
            _playerMovement.pressJump_Event?.AddListener(AddJump);
    }

    // Singleton
    public static AchievementsManager Instance;

    public List<Achievement> achievementsView;

    #endregion

    #region Private

    private void Awake()
    {
        if (Instance != null) Destroy(this.gameObject);

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (_passLvL && !achievementsView[0].yaSeActivoPa) achievementsView[0].Activate();
        
        if (_jumpCount == 100 && !achievementsView[1].yaSeActivoPa) achievementsView[1].Activate();
        if (_jumpCount == 200 && !achievementsView[2].yaSeActivoPa) achievementsView[2].Activate();
        if (_jumpCount == 300 && !achievementsView[3].yaSeActivoPa) achievementsView[3].Activate();
        if (_jumpCount == 450 && !achievementsView[4].yaSeActivoPa) achievementsView[4].Activate();
        if (_jumpCount == 700 && !achievementsView[5].yaSeActivoPa) achievementsView[5].Activate();
    }

    private int _jumpCount;
    
    private float _timeOfLvL1;
    private float _timeOfLvL2;
    private float _timeOfLvL3;

    private bool _playerIsDead;

    private bool _passLvL;

    private h_PlayerInteraction _player;
    private h_PlayerMovement _playerMovement;
    private EndLevel _endLevel;

    #endregion
}
