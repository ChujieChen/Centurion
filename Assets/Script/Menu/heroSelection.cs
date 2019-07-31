using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heroSelection : MonoBehaviour
{
    private float pickRadius = 5f;
    private GameObject UI;
    private GameObject hero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setUI(GameObject UI)
    {
        this.UI = UI;
        this.UI.GetComponent<SpriteRenderer>().enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            GameObject[] armyA = GameObject.FindGameObjectsWithTag("teamA");
            float minDistance = pickRadius;
            GameObject minCha = null;
            foreach (var cha in armyA)
            {
                float distance = Vector3.Distance(hit.point, cha.transform.position);
                if (distance < minDistance)
                {
                    minCha = cha;
                    minDistance = distance;
                }
            }
            if (minCha)
                UI.transform.position = minCha.transform.position;


            UI.GetComponent<SpriteRenderer>().enabled = minCha != null;
            hero = minCha;
        }
        if(Input.GetMouseButtonUp(0) && hero)
        {
            startGame();
        }
    }

    void startGame()
    {
        Time.timeScale = 1f;
        // start spawner
        GameObject spawner = GameObject.FindGameObjectWithTag("spawner");
        WaveSpawner spawnerScript = spawner.GetComponent<WaveSpawner>();
        spawnerScript.StartCountingDown();

        GameObject[] ui = GameObject.FindGameObjectsWithTag("UI");
        for (int i = 0; i < ui.Length; i++)
            Destroy(ui[i]);

        GameObject pickCamera = GameObject.FindGameObjectWithTag("MainCamera");
        Vector3 pos, dir;
        pos = pickCamera.transform.position;
        dir = pickCamera.transform.forward;
        //add cameras
        GameObject newCamera = Instantiate(Resources.Load("Prefabs/camera/3rdPersonCamera") as GameObject);
        //sync position
        newCamera.GetComponent<thridPersonCamera>().lookAt = hero.transform;
        newCamera.transform.position = pos;
        newCamera.transform.forward  = dir;
        newCamera.GetComponent<startMusic>().startGame();
        Instantiate(Resources.Load("Prefabs/SceneController/SceneController") as GameObject);
        Instantiate(Resources.Load("Prefabs/UI/pauseMenu") as GameObject);
        Instantiate(Resources.Load("Prefabs/UI/playerHealthBar") as GameObject);
        GameObject healthBar = Instantiate(Resources.Load("Prefabs/UI/healthBar") as GameObject);
        GameObject command   = hero.GetComponent<characterUpdater>().switchToPlayer();
        switchToAIControl();
        //this script is attached to this camera, delete it at last
        int oldBalance = pickCamera.GetComponent<CharacterPick>().balance;
        command.GetComponent<playerCommandController>().setBalance(oldBalance);
        command.GetComponent<playerCommandController>().setTextUI(healthBar.transform.Find("price").GetComponent<UnityEngine.UI.Text>());
        Destroy(pickCamera);

        sceneController.simpleAddHint("startGameHint");
        // Chujie added on July 18, 2019: cursor will be hidden, and lock at the center of the screen
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    void switchToAIControl()
    {
        List<GameObject> army = new List<GameObject>();
        GameObject[] armyA = GameObject.FindGameObjectsWithTag("teamA");
        GameObject[] armyB = GameObject.FindGameObjectsWithTag("teamB");
        foreach (var cha in armyA)
            army.Add(cha);
        foreach (var cha in armyB)
            army.Add(cha);
        foreach (var cha in army)
        {
            cha.GetComponent<characterUpdater>().switchToAIFirstTime();
            Transform parent = cha.transform.parent;
            if (parent && parent.name == "Defense")
            {
                cha.GetComponent<characterUpdater>().switchToAIDefense(cha.transform.position);
            }
        }
    }
}
