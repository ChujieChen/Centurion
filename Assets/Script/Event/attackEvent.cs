using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackEvent : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject parent;
    CharacterStats stat;
    AttackEventInterface attackEventInterface; 
    void Start()
    {
        attackEventInterface = parent.GetComponent<AttackEventInterface>();
        stat = parent.GetComponent<CharacterStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isEnemy(other.gameObject, parent))
        {
            attackEventInterface.hit();
            attackEventInterface.sepcialEffect(other);
            stat.attackEnemy(other.gameObject);
        }
    }
    static bool isEnemy(GameObject a, GameObject b)
    {
        if (a == b)
            return false;
        if ((a.tag == "teamA" || a.tag == "Player") && (b.tag == "teamB" || b.tag == "spawner"))
            return true;
        else if ((b.tag == "teamA" || b.tag == "Player") && (a.tag == "teamB" || a.tag == "spawner"))
            return true;
        return false;
    }

}
