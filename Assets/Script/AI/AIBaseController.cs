using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AIBaseController : MonoBehaviour, ControllerInterface
{
    protected Transform target;
    protected NavMeshAgent agent;
    protected Animator anim;
    protected CharacterStats stat;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        stat = GetComponent<CharacterStats>();
        anim = this.GetComponent<Animator>();
    }


    protected virtual void Update()
    {
        
    }

    protected void findNearestEnemy()
    {
        List<GameObject> enemyList = new List<GameObject>();
        if (this.tag == "teamA")
        {
            var temp = GameObject.FindGameObjectsWithTag("teamB");
            foreach (var o in temp)
                enemyList.Add(o);
        }
        else
        {
            var temp = GameObject.FindGameObjectsWithTag("teamA");
            foreach (var o in temp)
                enemyList.Add(o);
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player && player.GetComponent<CharacterStats>().currentHealth > 0)
                enemyList.Add(player);
        }

        Vector3 myPos = this.transform.position;
        float minDistance = float.MaxValue;
        Transform minTarget = null;
        foreach (var enemy in enemyList)
        {
            float distance = Vector3.Distance(myPos, enemy.transform.position);
            if (distance < minDistance)
            {
                minTarget = enemy.transform;
                minDistance = distance;
            }
        }
        target = minTarget;
    }

    public float getSpeedX()
    {
        return 0;
    }
    public float getSpeedY()
    {
        return agent.velocity.magnitude / agent.speed;
    }
    public bool getAttack()
    {
        if (target && agent)
        {
            Vector3 closestPoint = target.GetComponent<Collider>().ClosestPoint(this.transform.position);
            if (Vector3.Distance(closestPoint, this.transform.position) < stat.attackRange)
                return true;
        }
        return false;
    }

    protected void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3f);

    }

    protected virtual void changeHUD()
    {
    }
}
