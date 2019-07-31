using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public Slider playerHealthBar;
    GameObject player;
    CharacterStats playerStats;

    // Update is called once per frame
    void Update()
    {
        if (playerStats) {
            playerHealthBar.value = playerStats.currentHealth;
        } 
        else {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player)
            {
                playerStats = player.GetComponent<CharacterStats>();
                playerHealthBar.maxValue = playerStats.maxHealth;
                playerHealthBar.value = playerHealthBar.maxValue;
            }

        } 
    }
}
