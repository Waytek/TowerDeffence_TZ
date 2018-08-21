using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public Rigidbody rigidbody;
    public float timeToDestroy = 3;
    float damage;
    BaseGun gun;
    Action killAction;
    void Awake()
    {
        if (!rigidbody)
        {
            rigidbody = GetComponent<Rigidbody>();
        }
    }
	public void Shoot(BaseGun gun, float shootForce, float damage, Action killAction)
    {
        gameObject.SetActive(true);
        this.gun = gun;
        this.damage = damage;
        this.killAction = killAction;
        rigidbody.AddForce(transform.up*shootForce);

        Destroy(timeToDestroy);
    }
    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "Player" || coll.gameObject.layer == LayerMask.NameToLayer("Build"))
        {
            return;
        }
        if(coll.gameObject.tag == "enemy")
        {
            EnemyController enemy = coll.gameObject.GetComponent<EnemyController>();
            enemy.lifeController.SetDamage(damage,killAction);
            
        }
        Destroy(0);
    }
    void Destroy(float _timeToDestroy)
    {
        StartCoroutine(DestoyRoutine(_timeToDestroy));
    }
    IEnumerator DestoyRoutine(float _timeToDestory)
    {
        yield return new WaitForSeconds(_timeToDestory);
        gameObject.SetActive(false);
        rigidbody.isKinematic = true;
        rigidbody.isKinematic = false;
        gun.bulletPull.Add(this);
    }
}
