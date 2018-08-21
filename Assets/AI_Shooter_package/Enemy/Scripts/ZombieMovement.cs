using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour {
    EnemyController enemyController;
    EnemyAimController aimController;
  
    public float maxSpeed = 1;

    void Init()
    {
        enemyController = GetComponent<EnemyController>();
        if (!enemyController)
        {
            enemyController = gameObject.AddComponent<EnemyController>();
        }
        
        enemyController.animator.SetFloat("MaxSpeed", maxSpeed);
    }
    void Start()
    {
        Init();
        enemyController.onDead += OnDead;
        if(enemyController.target)
        enemyController.navAgent.SetDestination(enemyController.target.transform.position);
        Move(1);
    }
    void OnValidate()
    {
        if(Application.isPlaying)
        Init();
    }
    void Update()
    {

        if (enemyController.target)
        {
            if (Vector3.Distance(transform.position, enemyController.target.transform.position) <= enemyController.navAgent.stoppingDistance + 0.5f)
            {
                Move(0);
                //animator.SetBool("Atack", true);
            }
            else
            {
                Move(1);
                //animator.SetBool("Atack", false);
                enemyController.navAgent.SetDestination(enemyController.target.transform.position);
            }
        }
        else
        {
            Move(0);
        }
    }
    void Move(float speed)
    {
        enemyController.animator.SetFloat("Speed", speed);
    }
    void OnAnimatorMove()
    {
        if(enemyController)
            enemyController.navAgent.speed = (enemyController.animator.deltaPosition / Time.deltaTime).magnitude;
    }
    void OnDead()
    {
        this.enabled = false;
        enemyController.navAgent.enabled = false;
    }

}
