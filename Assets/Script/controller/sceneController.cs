using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneController : MonoBehaviour
{
    GameObject lastHint;
    float timePast = 0;
    private void Update()
    {
        updateCorpse();
        updateHint();
    }
    void updateCorpse()
    {
        GameObject[] corpse = GameObject.FindGameObjectsWithTag("Corpse");
        foreach (var c in corpse)
        {
            var childRenderer = c.GetComponentInChildren<Renderer>();
            var mat = childRenderer.material;
            var color = mat.color;
            color.a -= 1f / 60f / 3f;
            childRenderer.material.color = color;
            if (color.a < 0)
                Destroy(c);
        }
    }
    void updateHint()
    {
        if(lastHint)
        {
            if (timePast > 9)
            {
                Destroy(lastHint);
                lastHint = null;
            }
            else
            {
                var text = lastHint.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>();
                var newColor = text.color;
                if (timePast <= 6)
                {
                    var rotationTime = timePast % 2f;
                    if (rotationTime <= 1)
                        newColor.a = 1 - rotationTime;
                    else
                        newColor.a = rotationTime - 1;
                }
                else
                {
                    newColor.a = 1 - (timePast - 6) / 3;
                }
                text.color = newColor;
            }
            timePast += Time.deltaTime;
        }
    }

    void RestartGame(string str)
    {
        GameObject obj = GameObject.FindGameObjectWithTag("pauseMenu");
        var pauseMenu = obj.GetComponent<MainPauseMenuToggle>();
        pauseMenu.showPauseMenu();
        UnityEngine.UI.Text textComponent = pauseMenu.transform.GetChild(0).GetChild(2).transform.gameObject.GetComponent<UnityEngine.UI.Text>();
        textComponent.text = str;

        //menu fix
        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void addHint(string str)
    {
       if(lastHint)
        {
            Destroy(lastHint);
            lastHint = null;
        }
        timePast = 0;
        lastHint = Instantiate(Resources.Load("Prefabs/hintUI/" + str) as GameObject);
    }
    public static void simpleAddHint(string str)
    {
        GameObject.FindGameObjectWithTag("sceneController").GetComponent<sceneController>().addHint(str);
    }
}
