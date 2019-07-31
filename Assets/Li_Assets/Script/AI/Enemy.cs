using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    //PlayerManager playerManager;
    CharacterStats myStats;

    private void Start()
    {
        //playerManager = PlayerManager.instance;
        myStats = GetComponent<CharacterStats>();
    }

    public override void Interact()
    {
        base.Interact();
        //CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player)
        {
            CharacterCombat playerCombat = player.GetComponent<CharacterCombat>();

            if (playerCombat != null)
            {
                playerCombat.attack(myStats);
                
            }
            if (playerCombat == null)
            {
                Debug.Log("Player does not have CharacterCombat component.");
            }
        }
            
    }
}
