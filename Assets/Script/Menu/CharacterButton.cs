using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterButton : MonoBehaviour
{
    public void click()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CharacterPick>().setCharacter(this.name);
    }
}
