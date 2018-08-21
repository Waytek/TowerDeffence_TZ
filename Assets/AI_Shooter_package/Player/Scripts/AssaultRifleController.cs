using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifleController : BaseGun {
    public Transform bulletSpawnParent;
    public float armoryMaxCount;
    float armoryCurrentCount;
    public float bulletSpeed = 1;
    public float shootSpeed = 1;

    PlayerController playerController;
    void Init()
    {
        playerController = GetComponent<PlayerController>();
        if (!playerController)
        {
            playerController = gameObject.AddComponent<PlayerController>();
        }
        playerController.animator.SetFloat("ShootSpeed", shootSpeed);
    }
    void OnValidate()
    {
        if (Application.isPlaying)
            Init();

    }
    void Start()
    {
        Init();
        playerController.onDead += OnDead;
    }
    void Shoot()
    {
        if(armoryCurrentCount <= 0)
        {
            Reload();
            return;
        }
        if(bulletPull.Count == 0)
        {
            Bullet shootBullet = Instantiate(bullet, bulletSpawnParent);
            shootBullet.Shoot(this, bulletSpeed,Random.Range(minDamage,maxDamage),playerController.onKillEnemy);
            shootBullet.transform.SetParent(null);
        }else
        {
            bulletPull[0].transform.SetParent(bulletSpawnParent);
            bulletPull[0].transform.localPosition = Vector3.zero;
            bulletPull[0].transform.localRotation = bullet.transform.rotation;
            bulletPull[0].Shoot(this, bulletSpeed, Random.Range(minDamage, maxDamage), playerController.onKillEnemy);
            bulletPull[0].transform.SetParent(null);
            bulletPull.Remove(bulletPull[0]);
        }

    }
    void Reload()
    {
        armoryCurrentCount = armoryMaxCount;
    }
    void OnDead()
    {
        playerController.animator.SetLayerWeight(1, 0);
        enabled = false;
    }
}
