using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieStandartAtack : MonoBehaviour {
    EnemyController enemyController;

    public Transform atackPoint;

    public float atackDistance = 1f;

    public float atackSpeed = 1f;

    public float timeBetweenAtack = 0f;

    public float minDamage = 1;
    public float maxDamage = 5;

    float timeLastAtak = 0f;
    PlayerController playerController;
	// Use this for initialization
    void Init()
    {
        if (!atackPoint)
        {
            atackPoint = transform;
        }
        enemyController = GetComponent<EnemyController>();
        if (!enemyController)
        {
            enemyController = gameObject.AddComponent<EnemyController>();
        }

        enemyController.animator.SetFloat("AtackSpeed", atackSpeed);
        
        enemyController.navAgent.stoppingDistance = atackDistance;
    }
	void Start () {
        Init();
        enemyController.onDead += OnDead;
    }
    void OnValidate()
    {
        if (Application.isPlaying)
            Init();
    }

    // Update is called once per frame
    RaycastHit hit;
    void FixedUpdate()
    {

        if (Physics.Raycast(atackPoint.position, atackPoint.forward, out hit, atackDistance))
        {
            if (playerController == null)
            {             
                if (hit.transform.gameObject.tag == "Player")
                {
                    playerController = hit.transform.gameObject.GetComponent<PlayerController>();
                    Debug.DrawRay(atackPoint.position, atackPoint.forward, Color.green, atackDistance);
                    //Debug.Log("Found an object - distance: " + hit.distance);
                    Atack();
                }
                else
                {
                    StopAtack();
                }
            }
        }
        else
        {
            StopAtack();
        }
    }
    void Atack()
    {
        if (Time.time >= timeLastAtak + timeBetweenAtack)
        {
            if (!enemyController.animator.GetBool("Atack"))
            {
                enemyController.animator.SetBool("Atack", true);
            }
            timeLastAtak = Time.time;
            //StopAtack();
        }
        else
        {
            StopAtack();
        }
    }
    void StopAtack()
    {
        if (playerController)
        {
            playerController = null;
        }
        if (enemyController.animator.GetBool("Atack"))
            enemyController.animator.SetBool("Atack", false);
    }
    void Damage()
    {
        if (playerController)
        {
            playerController.lifeController.SetDamage(Random.Range(minDamage, maxDamage));
        }

    }
    void OnDead()
    {
        enabled = false;
    }
}
