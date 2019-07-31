using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class CharacterPick : MonoBehaviour
{
    string myPrefabName = "";
    int overUICount = 0;
    UnityEngine.UI.Text priceTag;
    public int balance;

    // Start is called before the first frame update
    void Start()
    {

    }
    public static void updatePrice(UnityEngine.UI.Text textUI, int balance)
    {
        textUI.text = "$" + balance;
    }
    public static bool canAfford(string prefabName, int balance)
    {
        return getPriceForCharacter(prefabName) <= balance;
    }
    public void setPriceTagUI(UnityEngine.UI.Text priceTag)
    {
        this.priceTag = priceTag;

        updatePrice(priceTag, balance);
    }
    // Update is called once per frame
    void Update()
    {
        if (overUICount > 0)
            return;

        RaycastHit hit;
        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if (Physics.SphereCast(ray, 1f, out hit, 100f))
        {
            GameObject range = GameObject.FindGameObjectWithTag("pickRange");
            Bounds b = new Bounds(range.transform.transform.position, range.transform.localScale);
            if (b.Contains(hit.point) && Input.GetMouseButtonUp(0))
                handleLeftClick(hit);
            if (Input.GetMouseButtonUp(1))
                handleRightClick(hit);
        }
    }

    public void setCharacter(string name)
    {
        myPrefabName = name;
    }

    public void onEnter()
    {
        overUICount += 1;
    }
    public void onExit()
    {
        overUICount -= 1;
    }
    void handleLeftClick(RaycastHit hit)
    {
        if (myPrefabName == "")
            return;
        string tempTag = hit.transform.tag;
        if (tempTag == "teamA")
            return;
        if (!canAfford(myPrefabName, balance))
            return;

        GameObject newGameObject = addCharacter(myPrefabName, hit.point);

        balance -= getPriceForCharacter(myPrefabName);
        updatePrice(priceTag, balance);
    }

    public static GameObject addCharacter(string prefabName, Vector3 pos)
    {
        string fileName = "Prefabs/character/" + prefabName;
        GameObject newGameObject = Instantiate(Resources.Load(fileName) as GameObject);

        NavMeshAgent agent = newGameObject.GetComponent<NavMeshAgent>();
        agent.Warp(pos);


        newGameObject.tag = "teamA";

        Quaternion newRotation = newGameObject.transform.rotation;
        newRotation *= Quaternion.Euler(Vector3.up);
        newGameObject.transform.rotation = newRotation;
        return newGameObject;
    }

    void handleRightClick(RaycastHit hit)
    {
        string clickTag = hit.transform.tag;
        if (clickTag != "teamA")
            return;
        balance += getPriceForCharacter(hit.transform.name);
        updatePrice(priceTag, balance);
        Destroy(hit.transform.gameObject);
    }


    static string getParsedName(string name)
    {
        string parsedName = string.Copy(name);
        int index = parsedName.IndexOf(' ');

        if (index >= 0)
            parsedName = parsedName.Substring(0, index);
        else
        {
            index = parsedName.IndexOf('(');
            if (index >= 0)
                parsedName = parsedName.Substring(0, index);
        }
        return parsedName;
    }

    public static int getPriceForCharacter(string name)
    {
        string parsedName = getParsedName(name);
        switch(parsedName)
        {
            case "swordManPrefab":
                return 100;
            case "hammerManPrefab":
                return 150;
            case "wizardPrefab":
                return 200;
        }
        return 0;
    }
    
}

