using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAimController : MonoBehaviour {
    EnemyController enemyController;

    public float speedRotation = 5;
    public float viewDistance = 10;
    //Rigidbody rigidbody;
    public Transform atackPoint;
    void Start()
    {
        enemyController = GetComponent<EnemyController>();
        if (!enemyController)
        {
            enemyController = gameObject.AddComponent<EnemyController>();
        }     
        if (!atackPoint)
        {
            atackPoint = transform;
        }
        enemyController.onDead += OnDead;
    }
    void Update()
    {
        if (enemyController.target)
        {
            LookToPoint(enemyController.target.transform.position - atackPoint.position);
        }
    }
    void LookToPoint(Vector3 lookPoint)
    {

        Quaternion needRotate = Quaternion.LookRotation(new Vector3(lookPoint.x, 0, lookPoint.z));
        transform.rotation = (Quaternion.Lerp(transform.rotation, needRotate, speedRotation / 100));       
    }
    void OnDead()
    {
        enabled = false;
    }
}
