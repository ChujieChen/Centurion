using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour, ControllerInterface
{
    float speedX = 0;
    float speedY = 0;
    bool  attack = false;

    public float getSpeedX()
    {
        return speedX;
    }
    public float getSpeedY()
    {
        return speedY;
    }
    public bool getAttack()
    {
        return attack;
    }

    void Update()
    {
        speedX = Input.GetAxis("Horizontal");
        speedY = Input.GetAxis("Vertical");
        attack = Input.GetMouseButtonDown(0);
    }
}
