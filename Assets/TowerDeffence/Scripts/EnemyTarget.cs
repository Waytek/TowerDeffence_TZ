using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "enemy")
        {
            if (TDGameController.Instance)
            {
                TDGameController.Instance.health -= 1;
            }
            Destroy(coll.gameObject);
        }
    }
}
