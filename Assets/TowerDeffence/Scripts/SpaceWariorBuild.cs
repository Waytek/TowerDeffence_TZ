using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceWariorBuild : Build {

    public Animator towerAnim;
    public AssaultRifleController rifleController;
    public AimController aimController;

    void Start()
    {
        towerAnim.enabled = false;
        aimController.enabled = false;
    }
    public override void ActivateTower(bool activate)
    {
        towerAnim.enabled = activate;
        aimController.enabled = activate;
    }
    public override void UpdateTower()
    {
        base.UpdateTower();
        rifleController.minDamage *= 1.3f;     //баланса нет, просто умножаю на 1.3 
        rifleController.maxDamage *= 1.3f;
    }
}
