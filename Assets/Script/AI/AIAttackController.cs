using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AIAttackController : AIBaseController
{
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        findNearestEnemy(); 
    }
    void Start()
    {
        changeHUD();
    }
    // Update is called once per frame
    protected override void Update()
    {
        // search nearest enemy 
        findNearestEnemy();

        if (!target && this.tag == "teamA")
        {
            GameObject tower = GameObject.FindGameObjectWithTag("spawner");
            if (tower)
                target = tower.transform;
            else
                return;
        }

        if (!target)
            return;
        Vector3 closestPoint = target.GetComponent<Collider>().ClosestPoint(this.transform.position);

        float distance = Vector3.Distance(this.transform.position, closestPoint);

        if (distance > stat.attackRange)
        {
            agent.SetDestination(closestPoint);
        }
        else if (distance <= stat.attackRange + 4)
        {
            FaceTarget();
        }
    }

    protected override void changeHUD()
    {
        var img = this.transform.Find("root").Find("UIPos").GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>();
        img.sprite = Resources.Load<Sprite>("Image/attack");
    }
}
