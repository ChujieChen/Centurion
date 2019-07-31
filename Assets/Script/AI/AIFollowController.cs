using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AIFollowController : AIBaseController
{
	float right;
	float front;
	GameObject player;
	// Start is called before the first frame update

	public void SetFollow(float right, float front, GameObject player)
	{
        this.right = right;
        this.front = front;
        this.player = player;
    }

    protected override void Awake()
	{
        base.Awake();
	}
    void Start()
    {
        changeHUD();
    }
    // Update is called once per frame
    protected override void Update()
	{
        findNearestEnemy(); // target
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

        if (distance <= stat.attackRange + 4)
        {
            FaceTarget();
        }
        follow();
	}

    void follow()
    {
        Vector3 newDestination = player.transform.position;
        newDestination += player.transform.right * right;
        newDestination += player.transform.forward * front;
        agent.SetDestination(newDestination);
    }

    protected override void changeHUD()
	{
		var img = this.transform.Find("root").Find("UIPos").GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>();
		img.sprite = Resources.Load<Sprite>("Image/follow");
	}
}
