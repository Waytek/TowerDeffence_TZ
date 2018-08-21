using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGun : MonoBehaviour {
    public Bullet bullet;
    public List<Bullet> bulletPull = new List<Bullet>();

    public float minDamage;
    public float maxDamage;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
