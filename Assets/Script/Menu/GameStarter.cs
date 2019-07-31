using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    public GameObject Level;
    public GameObject Main;
    public GameObject[] enemyList;

    public void PlayButtonPressed()
    {
        Level.SetActive(true);
        Main.SetActive(false);
    }
    public void QuitButtonPressed()
    {
        Application.Quit();
    }

    public void StartForest()
    {
        SceneManager.LoadScene("Base_Scene");
        SceneManager.sceneLoaded += startScene;
    }
    public void startScene(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= startScene;
        //load enemy
        foreach(var e in enemyList)
            Instantiate<GameObject>(e);
        //load camera
        GameObject cam = Instantiate(Resources.Load("Prefabs/camera/PickCamera") as GameObject);
        Instantiate(Resources.Load("Prefabs/camera/PickRange") as GameObject);
        Instantiate(Resources.Load("Prefabs/UI/line") as GameObject);
        //add buttons
        GameObject UIRoot = Instantiate(Resources.Load("Prefabs/UI/characterUI") as GameObject);
        GameObject UIpanel = UIRoot.transform.GetChild(0).gameObject;
        Object[] allChar = Resources.LoadAll("Prefabs/character");
        int buttonCounter = 0;
        foreach (var cha in allChar)
        {
            string chaName = cha.name;
            GameObject button = Instantiate(Resources.Load("Prefabs/UI/characterButton") as GameObject);
            button.name = chaName;
            button.transform.SetParent(UIpanel.transform, false);
            Vector3 newPos = button.transform.localPosition;
            newPos.x += buttonCounter * 100;
            button.transform.localPosition = newPos;
            UnityEngine.UI.RawImage img = button.GetComponentInChildren<UnityEngine.UI.RawImage>();
            img.texture = Resources.Load("icon/" + chaName) as Texture;
            UnityEngine.UI.Text UIText = button.GetComponentInChildren<UnityEngine.UI.Text>();
            UIText.text = CharacterPick.getPriceForCharacter(chaName).ToString();
            buttonCounter += 1;
        }

        //priceTag

        var textUI = UIRoot.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>();
        cam.GetComponent<CharacterPick>().setPriceTagUI(textUI);
    }

    public void startHeroSelection()
    {
        object[] armyA = GameObject.FindGameObjectsWithTag("teamA");
        //add some army into scene please
        if (armyA.Length == 0)
            return;
        GameObject pickCamera = GameObject.FindGameObjectWithTag("MainCamera");
        Destroy(GameObject.FindGameObjectWithTag("pickRange"));
        GameObject[] ui = GameObject.FindGameObjectsWithTag("UI");
        for (int i = 0; i < ui.Length; i++)
            Destroy(ui[i]);
        var heroSelection = pickCamera.AddComponent<heroSelection>();
        GameObject heroUI = Instantiate(Resources.Load("Prefabs/UI/heroSelection") as GameObject);
        heroSelection.setUI(heroUI);
        Instantiate(Resources.Load("Prefabs/UI/heroUI") as GameObject);
    }

}
