using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovePlayer : MonoBehaviour {
    PlayerController playerController;
    public List<MovePlayerHelper> helpers = new List<MovePlayerHelper>();
    public int quality = 10;
    public float speed = 0.5f;

    // Use this for initialization
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        if (!playerController)
        {
            playerController = gameObject.AddComponent<PlayerController>();
        }
        playerController.navAgent.speed = speed;
        InvokeRepeating("ChangeMovePoint",1,3);
        playerController.onDead += OnDead;
        playerController.onKillEnemy += ChangeMovePoint;      
    }
    void Update()
    {
        playerController.animator.SetFloat("Horizontal", playerController.navAgent.velocity.x);
        playerController.animator.SetFloat("Vertical", playerController.navAgent.velocity.z);
    }
    void ChangeMovePoint()
    {
        foreach (MovePlayerHelper helper in helpers)
        {
            helper.UpateWeight(quality);
        }
        helpers.Sort((helper1, helper2) => helper2.weight.CompareTo(helper1.weight));
        NavMeshHit hit;
        if (NavMesh.SamplePosition(Utils.GetRandomPositionInsideCollider(helpers[0].collid), out hit, 5f, NavMesh.AllAreas))
        {
            playerController.navAgent.SetDestination(hit.position);            
        }
        else
        {
            //ChangeMovePoint();
        }
    }
        void OnDead()
    {
        CancelInvoke("ChangeMovePoint");
        enabled = false;
        playerController.navAgent.enabled = false;
    }
}
