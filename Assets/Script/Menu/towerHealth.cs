using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class towerHealth : MonoBehaviour
{

    public Slider towerHealthBar;
    GameObject[] towers;
    float totalMaxHealth = 0;

    // Start is called before the first frame update
    void Start()
    {
        towers = GameObject.FindGameObjectsWithTag("spawner");
        if (towers.Length != 0)
        {
            foreach (GameObject tower in towers)
            {
                totalMaxHealth += tower.GetComponent<CharacterStats>().maxHealth;
            }
        }
        towerHealthBar.maxValue = totalMaxHealth;
        towerHealthBar.value = towerHealthBar.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        towers = GameObject.FindGameObjectsWithTag("spawner");
        float totalCurrentHealth = 0;
        foreach (GameObject tower in towers)
        {
            totalCurrentHealth += tower.GetComponent<CharacterStats>().currentHealth;
        }
        towerHealthBar.value = totalCurrentHealth;
    }
}
