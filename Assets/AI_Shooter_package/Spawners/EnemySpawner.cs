using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public EnemyController[] enemyPrefabs;
    Collider spawnColl;
    public Transform enemyTarget;
    public int spawnTime = 2;
//    public int remeaninEnemyCount;

    void Start()
    {
        spawnColl = GetComponent<Collider>();
        if(GameController.Instance)
            GameController.Instance.onStartGame += OnStartGame;
        if (TDGameController.Instance)
        {
            TDGameController.Instance.onNextWave += StartWave;
        }
    }
    void OnStartGame()
    {
    }
    public void StartWave(int wave, int enemyCount) {
        StartCoroutine(Spawn(wave, enemyCount));
    }
    public IEnumerator Spawn(int wave, int enemyCount)
    {
        currentLifeEnemy += enemyCount;
        while (enemyCount > 0)
        {
            enemyCount--;
            Vector3 spawnPos;
            if (spawnColl)
            spawnPos = Utils.GetRandomPositionInsideCollider(spawnColl);
            else
            {
                spawnPos = transform.position;
            }
            int minEnemyPrefab = (int)((float)enemyPrefabs.Length / (float)TDGameController.Instance.maxWaveCount * (float)wave)-1;
            minEnemyPrefab = minEnemyPrefab >= 0 ? minEnemyPrefab : 0;
            int maxEnemyPrefab = (int)((float)enemyPrefabs.Length / (float)TDGameController.Instance.maxWaveCount * (float)wave)+1;
            maxEnemyPrefab = maxEnemyPrefab < enemyPrefabs.Length ? maxEnemyPrefab : enemyPrefabs.Length;
            EnemyController newEnemy = Instantiate(enemyPrefabs[Random.Range(minEnemyPrefab, maxEnemyPrefab)], spawnPos, Quaternion.identity);
            newEnemy.target = enemyTarget;
            
                newEnemy.onDead += CheckEndWave;
            
            yield return new WaitForSeconds(spawnTime);
        }
        
    }
    int currentLifeEnemy;
    void CheckEndWave()
    {
        currentLifeEnemy--;

        if(currentLifeEnemy == 0)
        TDGameController.Instance.onEndWave.Invoke();
    }

}
