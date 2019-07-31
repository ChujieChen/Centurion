using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class characterUpdater : MonoBehaviour
{
    ControllerInterface controller;
    CharacterStats stat;

    Animator anim;
    Rigidbody rbody;

    bool isDead;


    void Awake()
    {
        isDead = false;
        controller = gameObject.AddComponent<IdleController>();
        anim  = this.GetComponent<Animator>();
        rbody = this.GetComponent<Rigidbody>();
        stat  = this.GetComponent<CharacterStats>();
    }

    void Update()
    {
        anim.SetFloat("speedX", controller.getSpeedX());
        anim.SetFloat("speedY", controller.getSpeedY());
        anim.SetBool("attack", controller.getAttack());
        anim.SetBool("death", isDead);
    }

    void OnAnimatorMove()
    {
        Vector3 newRootPosition = new Vector3(anim.rootPosition.x, this.transform.position.y, anim.rootPosition.z); 
        this.transform.position = Vector3.LerpUnclamped(this.transform.position, newRootPosition, stat.moveSpeed);
        rbody.MoveRotation(rbody.rotation * Quaternion.AngleAxis(controller.getSpeedX() * Time.deltaTime * stat.rotationSpeed, Vector3.up));
    }

    ControllerInterface GetAttackAIController()
    {
        return this.gameObject.AddComponent<AIAttackController>();
    }

    ControllerInterface GetDefenseAIController(Vector3 dest)
    {
        AIDefenseController ctrl = this.gameObject.AddComponent<AIDefenseController>();
        ctrl.SetDest(dest);
        return ctrl;
    }

    ControllerInterface GetFollowAIController(float right, float front, GameObject player)
    {
        AIFollowController ctrl = this.gameObject.AddComponent<AIFollowController>();
        ctrl.SetFollow(right, front, player);
        return ctrl;
    }


    public void switchToAIFirstTime()
    {
        Destroy((UnityEngine.Object)controller);
        controller = GetAttackAIController();
        GetComponent<NavMeshAgent>().enabled = true;
        addHUD();
    }

    public void switchToAIAttack()
    {
        Destroy((UnityEngine.Object)controller);
        controller = GetAttackAIController();
    }

    public void switchToAIDefense(Vector3 dest)
    {
        Destroy((UnityEngine.Object)controller);
        controller = GetDefenseAIController(dest);
        
    }

    public void switchToAIFollow()
    {
        Destroy((UnityEngine.Object)controller);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerCommandController c = GameObject.FindGameObjectWithTag("command").GetComponent<playerCommandController>();
        c.tryAddToNearBy(transform.gameObject);
        controller = GetFollowAIController(c.getRight(), 5, player);
    }
    public GameObject switchToPlayer()
    {
        gameObject.tag = "Player";
        Destroy((UnityEngine.Object)controller);
        Destroy(GetComponent<NavMeshAgent>());

        //add buff
        var heroStat = gameObject.GetComponent<CharacterStats>();
        float newSpeed = stat.moveSpeed;
        newSpeed = Mathf.Sqrt(newSpeed) * 1.8f;

        heroStat.moveSpeed = newSpeed;
        heroStat.attackPoint *= 2;
        heroStat.maxHealth *= 2;
        heroStat.currentHealth = stat.maxHealth;

        GameObject flag = Instantiate(Resources.Load("Prefabs/SceneController/flag") as GameObject);
        flag.transform.position = this.transform.position;

        controller = gameObject.AddComponent<PlayerMovementController>();
        GameObject command = Instantiate(Resources.Load("Prefabs/SceneController/commandRadius") as GameObject);
        Transform parent = gameObject.transform.Find("root");
        command.transform.SetParent(parent, false);
        command.GetComponent<playerCommandController>().setPlayer(gameObject);

        
        return command;
    }


    public void switchToIdle()
    {
        Destroy((UnityEngine.Object)controller);
        controller = gameObject.AddComponent<IdleController>();
    }

    public void setDeath()
    {
        isDead = true;
        GetComponent<characterUpdater>().switchToIdle();
        NavMeshAgent navmeshAgent = GetComponent<NavMeshAgent>();
        if (navmeshAgent)
            Destroy(navmeshAgent);
        if(gameObject.tag == "teamB")
        {
            GameObject command = GameObject.FindGameObjectWithTag("command");
            command.GetComponent<playerCommandController>().addBalance(CharacterPick.getPriceForCharacter(gameObject.name) / 2);
            GameObject[] spawner = GameObject.FindGameObjectsWithTag("spawner");
            foreach(var s in spawner)
                s.GetComponent<WaveSpawner>().onDeath(this.transform.gameObject);

            int chance = Random.Range(0, 5);
            if(chance == 0)
            {
                Debug.Log("DROP");
                GameObject hp = Instantiate(Resources.Load("Prefabs/SceneController/Potion_Health") as GameObject);
                var newPos = this.transform.position;
                newPos.y += 3;
                hp.transform.position = newPos;

            }
                
        }
        else if(gameObject.tag == "teamA")
        {
            GameObject command = GameObject.FindGameObjectWithTag("command");
            command.GetComponent<playerCommandController>().tryRemove(this.transform.gameObject);
        }

        gameObject.tag = "Corpse";
        setToAlphaMode();
    }
    void setToAlphaMode()
    {
        var childRenderer = GetComponentInChildren<Renderer>();
        var mat = childRenderer.material;
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.DisableKeyword("_ALPHABLEND_ON");
        mat.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = 3000;
    }

    void addHUD()
    {
        Transform UIPos = this.transform.Find("root").Find("UIPos");
        GameObject UI = Instantiate(Resources.Load("Prefabs/UI/characterHUD") as GameObject);
        UI.transform.SetParent(UIPos, false);
        //temp
        var img = UI.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>();
        if (this.tag == "teamA")
            img.color = new Color(0, 1, 1, 0.5f);
        else
            img.color = new Color(1, 0, 0, 0.5f);
    }
}
