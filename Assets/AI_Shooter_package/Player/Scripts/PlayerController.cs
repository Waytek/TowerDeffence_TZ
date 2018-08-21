using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseComponentController {

    public Action onKillEnemy = delegate { };
    // Use this for initialization
    void Start()
    {
        if(GameController.Instance)
        GameController.Instance.players.Add(this);
        onDead += OnDead;
        onKillEnemy += OnKillEnemy;
        if (GameController.Instance)
            GameController.Instance.onStartGame += OnStartGame;
    }
    void OnDead()
    {
        if (GameController.Instance)
        {
            GameController.Instance.players.Remove(this);
            GameController.Instance.Loose();
        }
    }
    void OnKillEnemy()
    {
        if (GameController.Instance)
            GameController.Instance.killedEnemyCount++;
    }
    void OnDisable()
    {        
        if(GameController.Instance)
        GameController.Instance.onStartGame -= OnStartGame;
    }
    void OnStartGame()
    {
        if (GameController.Instance)
            GameController.Instance.players.Remove(this);
        Destroy(this.gameObject);
    }

}
