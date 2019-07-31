using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ControllerInterface
{
    // Start is called before the first frame update
    float getSpeedX();
    float getSpeedY();
    bool  getAttack();
}
