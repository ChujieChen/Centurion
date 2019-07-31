using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkAllSpawner : MonoBehaviour
{
    // Update is called once per frame
    public float interval = 2.0f;
    private float count;

    private void Start()
    {
        count = interval;
    }

    void Update()
    {
        count -= Time.deltaTime;

        if (count <= 0)
        {
            count = interval;

            // check all spawners
            GameObject[] allSpawners = GameObject.FindGameObjectsWithTag("spawner");
            if (allSpawners.Length == 0)
            {
                Debug.Log("You win!!");
                GameObject obj = GameObject.FindGameObjectWithTag("pauseMenu");
                var pauseMenu = obj.GetComponent<MainPauseMenuToggle>();
                pauseMenu.showPauseMenu();
                UnityEngine.UI.Text textComponent = pauseMenu.transform.GetChild(0).GetChild(2).transform.gameObject.GetComponent<UnityEngine.UI.Text>();
                textComponent.text = "You win!";

                //menu fix
                Time.timeScale = 1f;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }

    }
}
