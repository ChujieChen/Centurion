using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerDestroyEffect : MonoBehaviour
{
    public GameObject DestroyedSpawner;
    CharacterStats spawnerStats;

    private void Start()
    {
        spawnerStats = GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnerStats.currentHealth <= 0)
        {
            Instantiate(DestroyedSpawner, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
