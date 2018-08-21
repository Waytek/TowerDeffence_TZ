using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour {
    PlayerController playerController;

    public float speedRotation = 5;
    public float viewDistance = 10;
    //Rigidbody rigidbody;
    public Transform gunPoint;
    public EnemyController target;
    public LayerMask raycastLayers;
    void Start()
    {       
        playerController = GetComponent<PlayerController>();
        if (!playerController)
        {
            playerController = gameObject.AddComponent<PlayerController>();
        }
        playerController.onKillEnemy += OnEnemyDead;
        if (!gunPoint)
        {
            gunPoint = transform;
        }
        playerController.onDead += OnDead;
        InvokeRepeating("FindTarget",1,1);
    }
    void Update()
    {
        if (target)
        {
            LookToPoint(target.transform.position - gunPoint.position);
        }
        else
        {
            if(playerController.animator.GetBool("Shoot"))
                playerController.animator.SetBool("Shoot", false);
            //FindTarget();
        }
                 
    }
    void LookToPoint(Vector3 lookPoint)
    {
        Quaternion needRotate = Quaternion.LookRotation(new Vector3(lookPoint.x,0,lookPoint.z));
        transform.rotation = (Quaternion.Lerp(transform.rotation, needRotate, speedRotation/100));
        
        float animRootate = (needRotate.ToEuler() - transform.rotation.ToEuler()).magnitude;
        if (!playerController.animator.GetBool("Shoot"))
        {
            if (animRootate < 0.1f)
            {
                playerController.animator.SetBool("Shoot", true);
            }
        }
        playerController.animator.SetFloat("Horizontal", animRootate);
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, viewDistance);
    }
    
    void OnEnemyDead()
    {     
        target = null;
        FindTarget();
    }
    List<EnemyController> enemyControllers = new List<EnemyController>();
    void FindTarget()
    {
        enemyControllers.Clear();
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, viewDistance, Vector3.one);

        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.gameObject.tag == "enemy")
            {
                enemyControllers.Add(hit.transform.GetComponent<EnemyController>());
            }
        }
        if (enemyControllers.Count > 0)
        {
            enemyControllers.Sort(
                (enemy1, enemy2) =>
                Vector3.Distance(transform.position, enemy1.transform.position).CompareTo(Vector3.Distance(transform.position, enemy2.transform.position))
                );            
            for(int i = 0; i < enemyControllers.Count; i++)
            {
                if (ObstaclesShoot(enemyControllers[i]))
                {
                    target = enemyControllers[i];
                    return;
                }
                
            }            
            target = null;        
        }
    }
    bool ObstaclesShoot(EnemyController enemy)
    {
        RaycastHit hit;
        Physics.Raycast(gunPoint.position, enemy.transform.position-gunPoint.position, out hit, viewDistance, raycastLayers.value);
        if (hit.transform)
        {
            if (hit.transform.tag == "enemy")
            {
                //target = enemyControllers[i];
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    void OnDead()
    {
        enabled = false;
    }
}
