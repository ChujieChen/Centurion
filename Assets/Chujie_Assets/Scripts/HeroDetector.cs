using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDetector : MonoBehaviour
{
    public GameObject ControlPosition;
    public GameObject Crosshair;
    public bool isInside;
    public GameObject HERO;
    private float timer = 0f;
    public bool underControl;

    // Start is called before the first frame update
    void Start()
    {
        isInside = false;
        underControl = false;
        this.GetComponent<AdvancedCatapultController>().enabled = false;
        this.GetComponent<AdvancedStoneForce>().enabled = false;
        HERO = null;
        SetColor(isInside, underControl);
    }

    private void FixedUpdate()
    {
        // right-click to activate
        if(isInside && Input.GetMouseButtonDown(1))
        {
            sceneController.simpleAddHint("catapult2");
            underControl = !underControl;
            SetColor(isInside, underControl);
            SetControl(underControl);
        }
        // L is only a backup and a test case
        if (isInside && Input.GetKey(KeyCode.L))
        {
            underControl = !underControl;
            SetColor(isInside, underControl);
            SetControl(underControl);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //print(other.ToString());
        if (other.gameObject.CompareTag("Player"))
        {
            //print("It is a player!");
            isInside = true;
            HERO = other.gameObject;
            SetColor(isInside, underControl);
            sceneController.simpleAddHint("catapult");
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    print("OnTriggerStay is working");
    //    if (isInside)
    //    {
    //        if (Input.GetKey(KeyCode.W))
    //        {
    //            underControl = !underControl;
    //            SetControl(underControl);
    //        }
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        //print("OnTriggerExit is working");
        if (other.gameObject.CompareTag("Player"))
        {
            isInside = false;
            if(underControl)
            {
                SetControl(!underControl);
            }
            underControl = false;
            SetColor(isInside, underControl);
        }
            
    }

    private void SetControl(bool undercontrol)
    {
        GetComponent<AdvancedCatapultController>().enabled = undercontrol;
        GetComponent<AdvancedStoneForce>().enabled = undercontrol;
        HERO.GetComponent<characterUpdater>().enabled = !undercontrol;
        var ani = HERO.GetComponent<Animator>();

        ani.SetFloat("speedX", 0);
        ani.SetFloat("speedY", 0);
        ani.SetBool("attack", false);
    }
    private void SetColor(bool isinside, bool undercontrol)
    {
        float position_alpha = 1.0f;
        float crosshair_alpha = 1.0f;
        if (isinside && undercontrol)
        {
            position_alpha = 1f;
            crosshair_alpha = 1f;
        }
        else if(isinside && !undercontrol)
        {
            position_alpha = 1f;
            crosshair_alpha = 0.2f;
        }
        else
        {
            position_alpha = 0.2f;
            crosshair_alpha = 0.2f;
        }
        SpriteRenderer sr = ControlPosition.GetComponent<SpriteRenderer>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, position_alpha);
        sr = Crosshair.GetComponent<SpriteRenderer>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, crosshair_alpha);
    }
}
