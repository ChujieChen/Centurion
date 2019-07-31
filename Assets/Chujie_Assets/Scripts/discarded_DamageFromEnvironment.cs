using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFromEnvironment : MonoBehaviour
{
    public int damage = 200;
    CharacterStats myStats;

    private void Start()
    {
        //playerManager = PlayerManager.instance;
        myStats = GetComponent<CharacterStats>();
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag.Equals("environmentDamage"))
        { 
            myStats.getDamage(damage);
        }
    }

    //private void OnCollisionStay(Collider c)
    //{
    //    if (c.gameObject.tag.Equals("environmentDamage"))
    //    {
    //        myStats.TakeDamege(damage);
    //    }
    //}
}
