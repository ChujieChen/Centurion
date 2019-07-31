using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCommandController : MonoBehaviour
{
    GameObject player;
    SortedDictionary<int, GameObject> nearbyArmy;
    UnityEngine.UI.Text textUI;
    int balance;

    // added by Chujie
    public AudioClip cashRegister;
    public AudioClip orderAttack;
    public AudioClip orderFollow;
    public AudioClip orderDefence;

    void Start()
    {
        player.GetComponent<Animator>();
        nearbyArmy = new SortedDictionary<int, GameObject>();
    }
    public void setBalance(int balance)
    {
        this.balance = balance;
    }
    public void addBalance(int balance)
    {
        var oldBalance = this.balance;
        this.balance += balance;
        CharacterPick.updatePrice(textUI, this.balance);
        if (oldBalance % 1000 > this.balance % 1000)
            sceneController.simpleAddHint("buyArmyEvent");
    }
    public void setTextUI(UnityEngine.UI.Text textUI)
    {
        this.textUI = textUI;
        CharacterPick.updatePrice(textUI, balance);
    }
    public void setPlayer(GameObject player)
    {
        this.player = player;
    }
    private void OnTriggerEnter(Collider other)
    {
        tryAddToNearBy(other.gameObject);
    }

    public void tryAddToNearBy(GameObject obj)
    {
        if(obj.tag != "teamA")
            return;
        GameObject army = null;
        nearbyArmy.TryGetValue(obj.GetInstanceID(), out army);
        if (!army)
            nearbyArmy.Add(obj.GetInstanceID(), obj.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.tag != "teamA")
            return;
        tryRemove(obj);
    }

   public void tryRemove(GameObject obj)
    {
        GameObject army = null;
        nearbyArmy.TryGetValue(obj.GetInstanceID(), out army);
        if (army)
            nearbyArmy.Remove(obj.GetInstanceID());
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Comma))
        {
            commandToAttack();
            GetComponent<AudioSource>().PlayOneShot(orderAttack);
        }
            
        else if (Input.GetKeyUp(KeyCode.Period))
        {
            commandToDefense(); 
            GetComponent<AudioSource>().PlayOneShot(orderDefence);
        }
            
        else if (Input.GetKeyUp(KeyCode.Slash))
        {
            commandToFollow(); 
            GetComponent<AudioSource>().PlayOneShot(orderFollow);
        }
            

        if (Input.GetKeyUp(KeyCode.Alpha8))
            tryAdd("swordManPrefab");
        if (Input.GetKeyUp(KeyCode.Alpha9))
            tryAdd("hammerManPrefab");
        if (Input.GetKeyUp(KeyCode.Alpha0))
            tryAdd("wizardPrefab");
    }
    void commandToAttack()
    {
        IDictionaryEnumerator e = nearbyArmy.GetEnumerator();
        while (e.MoveNext())
        {
            GameObject o = e.Value as GameObject;
            o.GetComponent<characterUpdater>().switchToAIAttack();
        }
    }

    void commandToDefense()
    {
        int size = nearbyArmy.Count;
        Vector3 dir = player.transform.right;
        int i = 0;
        IDictionaryEnumerator e = nearbyArmy.GetEnumerator();
        while (e.MoveNext())
        {
            Vector3 dest = player.transform.position + player.transform.forward * 2;
            if(i % 2 == 1)
            {
                dest += (i / 2 + 1) * (-dir);
            }
            else
            {
                dest += (i / 2 + 1) * dir;
            }
            GameObject o = e.Value as GameObject;
            o.GetComponent<characterUpdater>().switchToAIDefense(dest);
            i++;
        }
    }

    static float getRgihtValue(int index)
    {
        if(index % 2 == 1)
            return 0.5f  + index / 2;
        else
            return -0.5f - index / 2;
    }
    public float getRight()
    {
        return getRgihtValue(nearbyArmy.Count - 1);
    }

    void commandToFollow()
    {
        int size = nearbyArmy.Count;
        Vector3 dir = player.transform.right;
        int i = 0;
        IDictionaryEnumerator e = nearbyArmy.GetEnumerator();
        while (e.MoveNext())
        { 
            GameObject o = e.Value as GameObject;
            o.GetComponent<characterUpdater>().switchToAIFollow();
            i++;
        }
    }


    void tryAdd(string prefabName)
    {
        if (CharacterPick.canAfford(prefabName, balance))
        {
            GameObject newArmy = CharacterPick.addCharacter(prefabName, GameObject.FindGameObjectWithTag("flag").transform.position);
            balance -= CharacterPick.getPriceForCharacter(prefabName);
            CharacterPick.updatePrice(textUI, balance);
            newArmy.GetComponent<characterUpdater>().switchToAIFirstTime();
            newArmy.GetComponent<characterUpdater>().switchToAIFollow();
            // added by Chujie
            GetComponent<AudioSource>().PlayOneShot(cashRegister);
        }
    }
}
