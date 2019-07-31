using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroControlCatapult : MonoBehaviour
{

    public GameObject Hero;
    public bool heroInside;
    public bool underControl;
    // Start is called before the first frame update
    void Start()
    {
        heroInside = this.GetComponent<HeroDetector>().isInside;
        underControl = false;
        this.GetComponent<AdvancedCatapultController>().enabled = false;
        this.GetComponent<AdvancedStoneForce>().enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        heroInside = this.GetComponent<HeroDetector>().isInside;
        if (heroInside)
        {
            Hero = this.GetComponent<HeroDetector>().HERO;
        }
        if (heroInside && Input.GetKeyUp(KeyCode.Mouse1))
        {
            underControl = !underControl;
        }
        this.GetComponent<AdvancedCatapultController>().enabled = underControl;
        this.GetComponent<AdvancedStoneForce>().enabled = underControl;
        Hero.GetComponent<characterUpdater>().enabled = !underControl;
    }
}
