using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovePlayerHelper : MonoBehaviour {
    public int enemyCount;
    public float weight;
    public Collider collid;
    void Start()
    {
        collid = GetComponent<Collider>();
    }
    int previosQuality = 10;
    public void UpateWeight(int quality)
    {
        previosQuality = quality;
        weight = 0;
        for (int i = 0; i < quality; i++)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(Utils.GetRandomPositionInsideCollider(collid), out hit, 3f, NavMesh.AllAreas))
            {
                weight++;
            }
        }        
            weight /= enemyCount+1;
    }
	// Use this for initialization
	void OnTriggerEnter(Collider coll)
    {
        UpateWeight(previosQuality);
        if (coll.gameObject.tag == "enemy")
        {
            enemyCount++;
            EnemyController enemy = coll.GetComponent<EnemyController>();
            enemy.onDead += OnEnemyDead;
        }
    }
    void OnTriggerExit(Collider coll)
    {
        UpateWeight(previosQuality);
        if (coll.gameObject.tag == "enemy")
        {
            enemyCount--;
            EnemyController enemy = coll.GetComponent<EnemyController>();
            enemy.onDead -= OnEnemyDead;
        }
    }
    void OnEnemyDead()
    {
        UpateWeight(previosQuality);
        enemyCount--;
    }
}
