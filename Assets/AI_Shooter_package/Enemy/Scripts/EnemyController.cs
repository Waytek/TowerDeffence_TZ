using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseComponentController {
    PlayerController _target;
    List<PlayerController> players = new List<PlayerController>();
    public Transform target;
    public int goldForDead = 10;

    void Start()
    {
        onDead += OnDead;
        if (GameController.Instance)
        GameController.Instance.onStartGame += OnStartGame;
    }
    void OnDisable()
    {
        onDead -= OnDead;
        if (GameController.Instance)
            GameController.Instance.onStartGame -= OnStartGame;
    }
    void OnDead()
    {
        Destroy(this.gameObject, 3);
        if (TDGameController.Instance)
        {
            TDGameController.Instance.gold += goldForDead;
        }
    }
    void OnStartGame()
    {
        Destroy(this.gameObject);
        
    }

}
