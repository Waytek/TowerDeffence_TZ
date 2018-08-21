using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController Instance;
    public Camera startedCamera;
    public List<PlayerController> players = new List<PlayerController>();
    public int maxEnemyCount = 30;
    public int remeaninEnemyCount;
    public Action onStartGame = delegate { };
    public Action onWin = delegate { };
    public Action onLoose = delegate { };
    public Action<int> onChangeRemeaningEnemy = delegate { };
    private int _killedEnemyCount;
    public int killedEnemyCount
    {
        get { return _killedEnemyCount; }
        set
        {
            
            _killedEnemyCount = value;
            onChangeRemeaningEnemy.Invoke(maxEnemyCount - _killedEnemyCount);
            if (_killedEnemyCount >= maxEnemyCount)
            {
                Win();
            }
        }
    }

    public List<PlayerSpawnPoint> playerSpawners = new List<PlayerSpawnPoint>();

    void Awake()
    {
        Instance = this;
    }
    void Start () {
        
        onStartGame += SpawnPlayer;
    }
    public void SpawnPlayer()
    {
        if(playerSpawners.Count > 0)
        playerSpawners[UnityEngine.Random.Range(0, playerSpawners.Count)].Spawn();
        else
        {
            Debug.LogError("No Player Spawners");
        }
    }
    public void Win()
    {        
        onWin.Invoke();
    }
    public void Loose()
    {
        onLoose.Invoke();
    }
    public void StartGame()
    {
        if (startedCamera)
        {
            Destroy(startedCamera.gameObject);
        }
        remeaninEnemyCount = maxEnemyCount;
        killedEnemyCount = 0;
        onStartGame.Invoke();        
    }
	

}
