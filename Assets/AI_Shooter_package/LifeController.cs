using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour {

    public BaseComponentController controller;
    // Use this for initialization
    public float maxHitPoints = 100;

    float curentHitPoints;
    void Start()
    {
        controller = GetComponent<BaseComponentController>();
        if (!controller)
        {
            controller = gameObject.AddComponent<BaseComponentController>();
        }
        curentHitPoints = maxHitPoints;

    }
    bool isDead;
    void Dead()
    {
        controller.animator.SetBool("Dead", true);
        GetComponent<Collider>().enabled = false;
        controller.onDead.Invoke();
        isDead = true;
    }
    void OnDestroy()
    {
        if(!isDead)
        controller.onDead.Invoke();
    }
    public void SetDamage(float damage, Action onDead)
    {
        curentHitPoints -= damage;
        if (curentHitPoints <= 0)
        {
            Dead();
            onDead.Invoke();
        }
    }
    public void SetDamage(float damage)
    {
        curentHitPoints -= damage;
        if (curentHitPoints <= 0)
        {
            Dead();
        }
    }
}
